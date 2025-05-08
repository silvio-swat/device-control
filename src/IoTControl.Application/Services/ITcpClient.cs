using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace IoTControl.Infrastructure.Services
{

    public interface ITcpClient : IDisposable
    {
        Task ConnectAsync(string host, int port, CancellationToken cancellationToken);
        Stream GetStream();
    }

}