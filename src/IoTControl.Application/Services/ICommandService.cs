// IoTControl.Application/Services/ICommandService.cs
using IoTControl.Application.Services;

public interface ICommandService
{
    Task<string> ExecuteCommand(string deviceId, string command, string[] parameters);
}

// IoTControl.Application/Services/CommandService.cs
public class CommandService : ICommandService
{
    private readonly ITelnetService _telnetService;
    private readonly IDeviceService _deviceService;

    public CommandService(ITelnetService telnetService, IDeviceService deviceService)
    {
        _telnetService = telnetService;
        _deviceService = deviceService;
    }

    public async Task<string> ExecuteCommand(string deviceId, string command, string[] parameters)
    {
        var device = await _deviceService.GetDeviceById(deviceId);
        return await _telnetService.SendCommandAsync(device.Url, command, parameters);
    }
}