using System.Text.Json.Serialization;

namespace IoTControl.Domain.Models
{
    public class Device
    {
        public string Identifier { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public List<CommandDescription> Commands { get; set; } = new List<CommandDescription>();
    }

    public class CommandDescription
    {
        public string Operation { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Command Command { get; set; } = new Command();
        public string Result { get; set; } = string.Empty;
        public string Format { get; set; } = string.Empty;
    }

    public class Command
    {
        [JsonPropertyName("command")]
        public string CommandText { get; set; } = string.Empty;
        public List<Parameter> Parameters { get; set; } = new List<Parameter>();
    }

    public class Parameter
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
