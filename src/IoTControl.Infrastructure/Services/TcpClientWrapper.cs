using System.Net.Sockets;

namespace IoTControl.Infrastructure.Services
{
    public class TcpClientWrapper : ITcpClient
    {
        private readonly TcpClient _tcpClient = new();

        public Task ConnectAsync(string host, int port, CancellationToken cancellationToken)
        {
            return _tcpClient.ConnectAsync(host, port, cancellationToken).AsTask();
        }

        public Stream GetStream()
        {
            return _tcpClient.GetStream();
        }

        public void Dispose()
        {
            _tcpClient.Dispose();
        }
    }
}