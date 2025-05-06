// IoTControl.Application/Services/IDeviceService.cs
using System.Net.Http.Json;
using IoTControl.Domain.Models; // Adicione esta using

public interface IDeviceService
{
    Task<IEnumerable<string>> GetDevices();
    Task<Device> GetDeviceById(string id);
}

// IoTControl.Application/Services/DeviceService.cs
public class DeviceService : IDeviceService
{
    private readonly HttpClient _httpClient;

    public DeviceService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<string>> GetDevices()
    {
        var response = await _httpClient.GetAsync("/device");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<string>>();
    }

    public async Task<Device> GetDeviceById(string id)
    {
        var response = await _httpClient.GetAsync($"/device/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Device>();
    }
}