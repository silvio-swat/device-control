using IoTControl.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoTControl.API.Controllers
{
    // IoTControl.API/Controllers/DevicesController.cs
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Este atributo protege todos os endpoints deste controller
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DevicesController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var devices = await _deviceService.GetDevices();
            return Ok(devices);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var device = await _deviceService.GetDeviceById(id);
            return Ok(device);
        }
    }

    // IoTControl.API/Controllers/CommandsController.cs
    [ApiController]
    [Route("api/[controller]")]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandService _commandService;

        public CommandsController(ICommandService commandService)
        {
            _commandService = commandService;
        }

        [HttpPost("{deviceId}")]
        public async Task<IActionResult> ExecuteCommand(string deviceId, [FromBody] CommandRequest request)
        {
            var result = await _commandService.ExecuteCommand(deviceId, request.Command, request.Parameters);
            return Ok(new { result });
        }
    }

    public record CommandRequest(string Command, string[] Parameters);
}
