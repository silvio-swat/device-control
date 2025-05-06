using System.Threading.Tasks;
using System.Threading;

namespace IoTControl.Application.Services
{
    public interface ITelnetService
    {
        Task<string> SendCommandAsync(string url, string command, string[] parameters, CancellationToken cancellationToken = default);
    }
}
