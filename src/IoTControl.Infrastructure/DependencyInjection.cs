using Microsoft.Extensions.DependencyInjection;
using IoTControl.Application.Services; // Para a interface ITelnetService
using IoTControl.Infrastructure.Services; // Para a implementação TelnetService

namespace IoTControl.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // 1) Registrar ITcpClient primeiro
            services.AddTransient<ITcpClient, TcpClientWrapper>();

            // 2) Registrar serviços que dependem dele
            services.AddScoped<ITelnetService, TelnetService>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<ICommandService, CommandService>();
            services.AddHttpClient<IDeviceService, DeviceService>(client =>
            {
                client.BaseAddress = new Uri("https://2f8e4356-9f20-47af-bb45-d9f86a3b8858.mock.pstmn.io");
            });


            return services;
        }
    }
}
