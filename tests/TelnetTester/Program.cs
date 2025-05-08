using Microsoft.Extensions.DependencyInjection;
using IoTControl.Application.Services;
using IoTControl.Infrastructure;          // para AddInfrastructure()

// 1) Defina a interface ITcpClient (caso não exista no seu projeto)
public interface ITcpClient
{
    Task ConnectAsync(string host, int port, CancellationToken cancellationToken = default);
    System.Net.Sockets.NetworkStream GetStream();
}

// 2) Implente um wrapper sobre TcpClient
public class TcpClientWrapper : ITcpClient
{
    private readonly System.Net.Sockets.TcpClient _client = new();
    public async Task ConnectAsync(string host, int port, CancellationToken cancellationToken = default)
        => await _client.ConnectAsync(host, port, cancellationToken);
    public System.Net.Sockets.NetworkStream GetStream() => _client.GetStream();
}

public class Program
{
    public static async Task Main(string[] args)
    {
        // 3) Configura DI
        var services = new ServiceCollection();
        services.AddTransient<ITcpClient, TcpClientWrapper>();
        services.AddInfrastructure();  // Injetar TelnetService

        var provider = services.BuildServiceProvider();
        var telnet = provider.GetRequiredService<ITelnetService>();

        Console.WriteLine("Enviando Device e Comando  via mock Ncat (localhost:2323)...");
        try
          {
            string response = await telnet.SendCommandAsync(
                "localhost:2323",       // host:porta do mock Ncat
                "SET_BRIGHTNESS DEVICE003",  // Command espaço DEVICE
                new[] { "75" }           // param (Somente para casos de que o comando envia parametros)
            );
        Console.WriteLine($"Resposta: {response}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Erro: {ex.Message}");
        }
    }
}
