namespace IoTControl.API.Models
{
    public class ExecuteCommandRequest
    {
        public string DeviceId { get; set; } = string.Empty;
        public Dictionary<string, string> Parameters { get; set; } = new();
    }
}