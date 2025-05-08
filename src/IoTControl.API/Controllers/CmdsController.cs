using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using IoTControl.API.Models;
using IoTControl.Application.Services;
using IoTControl.Domain.Models;          // Seu modelo Device e CommandDescription
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoTControl.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CmdsController : ControllerBase
    {
        private readonly ITelnetService _telnetService;
        private readonly IDeviceService _deviceService;

        public CmdsController(ITelnetService telnetService, IDeviceService deviceService)
        {
            _telnetService = telnetService;
            _deviceService = deviceService;
        }

        [HttpPost("{commandName}")]
        public async Task<IActionResult> ExecuteCommand(
            string commandName,
            [FromBody] ExecuteCommandRequest request)
        {
            // 1) Validação básica do request
            if (string.IsNullOrWhiteSpace(request.DeviceId))
                return BadRequest("DeviceId é obrigatório.");

            // 2) Busca o dispositivo
            var device = await _deviceService.GetDeviceById(request.DeviceId);

            var options = new JsonSerializerOptions { WriteIndented = true };
            string deviceJson = JsonSerializer.Serialize(device, options);
            Console.WriteLine("Device carregado:\n" + deviceJson);


            if (device == null)
                return NotFound($"Dispositivo '{request.DeviceId}' não encontrado.");

            // 3) Localiza o CommandDescription pelo nome (case-insensitive)
            var commandDesc = device.Commands
                .FirstOrDefault(c =>
                    c.Operation.Equals(commandName, StringComparison.OrdinalIgnoreCase));
            if (commandDesc == null)
                return BadRequest($"Comando '{commandName}' não encontrado no dispositivo '{request.DeviceId}'.");

            // 4) Extrai os parâmetros na ordem correta
            string[] paramValues;
            try
            {
                paramValues = commandDesc.Command.Parameters
                    .Select(p =>
                        request.Parameters.TryGetValue(p.Name, out var value)
                            ? value
                            : throw new ArgumentException($"Parâmetro '{p.Name}' obrigatório."))
                    .ToArray();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            // 5) Executa o comando via Telnet
            string result;
            try
            {
                result = await _telnetService.SendCommandAsync(
                    "localhost:2323",       // host:porta do mock
                    commandDesc.Command.CommandText + ' ' + request.DeviceId, // usa a propriedade CommandText
                    paramValues);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao executar comando: {ex.Message}");
            }

            // 6) Retorna o resultado puro (string) para o cliente
            return Ok(result);
        }
    }
}
