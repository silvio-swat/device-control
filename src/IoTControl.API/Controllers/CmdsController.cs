using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IoTControl.API.Models;
using IoTControl.Application.Services;
using IoTControl.Domain.Models;

namespace IoTControl.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CmdsController : ControllerBase
    {
        private const string HostAddress = "localhost:2323";
        private readonly ITelnetService _telnetService;
        private readonly IDeviceService _deviceService;
        private readonly ILogger<CmdsController> _logger;

        public CmdsController(
            ITelnetService telnetService,
            IDeviceService deviceService,
            ILogger<CmdsController> logger)
        {
            _telnetService = telnetService;
            _deviceService = deviceService;
            _logger = logger;
        }

        [HttpPost("{commandName}")]
        public async Task<IActionResult> ExecuteCommand(string commandName, [FromBody] ExecuteCommandRequest request)
        {
            // Guard Clauses: falhar cedo :contentReference[oaicite:3]{index=3}
            if (request is null)
                return BadRequest($"{nameof(request)} não pode ser nulo.");
            if (string.IsNullOrWhiteSpace(request.DeviceId))
                return BadRequest($"{nameof(request.DeviceId)} é obrigatório.");

            // 1) Busca o dispositivo
            var device = await _deviceService.GetDeviceById(request.DeviceId);
            if (device is null)
                return NotFound($"Dispositivo '{request.DeviceId}' não encontrado.");

            // 3) Localiza o comando (case-insensitive)
            var commandDesc = device.Commands
                .FirstOrDefault(c => c.Operation.Equals(commandName, StringComparison.OrdinalIgnoreCase));
            if (commandDesc is null)
                return BadRequest($"Comando '{commandName}' não encontrado no dispositivo '{request.DeviceId}'.");

            // 4) Extrai parâmetros
            if (!TryExtractParameters(commandDesc, request.Parameters, out var paramValues, out var paramError))
                return BadRequest(paramError);

            // 5) Executa o comando via Telnet — **não alterado** :contentReference[oaicite:5]{index=5}
            string result;
            try
            {
                result = await _telnetService.SendCommandAsync(
                    HostAddress,
                    commandDesc.Command.CommandText + ' ' + request.DeviceId,
                    paramValues);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro no SendCommandAsync para {DeviceId}/{Command}", request.DeviceId, commandName);
                return StatusCode(500, $"Erro ao executar comando: {ex.Message}");
            }

            // 6) Retorna resultado
            return Ok(result);
        }

        private bool TryExtractParameters(
            CommandDescription commandDesc,
            IDictionary<string, string> provided,
            out string[] values,
            out string? errorMessage)
        {
            try
            {
                values = commandDesc.Command.Parameters
                    .Select(p => provided.TryGetValue(p.Name, out var v) && !string.IsNullOrWhiteSpace(v)
                        ? v
                        : throw new ArgumentException($"Parâmetro '{p.Name}' obrigatório."))
                    .ToArray();
                errorMessage = null;
                return true;
            }
            catch (ArgumentException ex)
            {
                values = Array.Empty<string>();
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}
