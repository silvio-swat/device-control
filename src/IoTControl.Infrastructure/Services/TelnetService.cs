using System.Text;
using IoTControl.Application.Services;

namespace IoTControl.Infrastructure.Services
{
    public class TelnetService : ITelnetService
    {
        private readonly ITcpClient _tcpClient;

        public TelnetService(ITcpClient tcpClient)
        {
            _tcpClient = tcpClient;
        }


        public async Task<string> SendCommandAsync(
            string url, string command, string[] parameters,
            CancellationToken cancellationToken = default)
        {
            var parts = url.Split(':');
            if (parts.Length != 2 || !int.TryParse(parts[1], out int port))
                throw new ArgumentException("URL inválida...", nameof(url));

            await _tcpClient.ConnectAsync(parts[0], port, cancellationToken);
            using var stream = _tcpClient.GetStream();

            // 1) Monta payload com CRLF
            string payload = $"{command} {string.Join(" ", parameters)}\r\n";
            byte[] data = Encoding.ASCII.GetBytes(payload);

            // 2) Envia e força flush
            await stream.WriteAsync(data, 0, data.Length, cancellationToken);
            await stream.FlushAsync(cancellationToken);

            // 3) Lê resposta
            var buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
            return Encoding.ASCII.GetString(buffer, 0, bytesRead).TrimEnd('\r', '\n');
        }

    }
}