using System.Net.Http.Json;
using IoTControl.Domain.Models; // Adicione esta using

public interface IDeviceService
{
    Task<IEnumerable<string>> GetDevices();
    Task<Device> GetDeviceById(string id);
}

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

        var devices = await response.Content.ReadFromJsonAsync<List<string>>();
        if (devices == null)
        {
            throw new InvalidOperationException("Resposta inesperada: conteúdo nulo.");
        }
        return devices;
    }

    public async Task<Device> GetDeviceById(string id)
    {
        var response = await _httpClient.GetAsync($"/device/{id}");
        response.EnsureSuccessStatusCode();

        var device = await response.Content.ReadFromJsonAsync<Device>();
        if (device == null)
        {
            throw new InvalidOperationException("Resposta inesperada: conteúdo nulo.");
        }
        return device;
    }
}