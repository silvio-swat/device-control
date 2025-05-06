using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IoTControl.Application.Services;

namespace IoTControl.Infrastructure.Services
{
    public class TelnetService : ITelnetService
    {
        public async Task<string> SendCommandAsync(string url, string command, string[] parameters, CancellationToken cancellationToken = default)
        {
            using var tcpClient = new TcpClient();

            var parts = url.Split(':');
            if (parts.Length != 2 || !int.TryParse(parts[1], out int port))
            {
                throw new ArgumentException("URL inválida. Formato esperado: 'host:porta'.", nameof(url));
            }
            var host = parts[0];

            await tcpClient.ConnectAsync(host, port, cancellationToken);

            using var stream = tcpClient.GetStream();

            string payload = $"{command} {string.Join(" ", parameters)}\r\n";
            byte[] data = Encoding.ASCII.GetBytes(payload);

            await stream.WriteAsync(data, 0, data.Length, cancellationToken);

            // Lê a resposta até encontrar o terminador '\r'
            var buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
            return Encoding.ASCII.GetString(buffer, 0, bytesRead).TrimEnd('\r');
        }
    }
}