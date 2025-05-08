using System.Net;
using System.Text;
using IoTControl.Application.Services;
using Moq;
using Moq.Protected;
using IoTControl.Domain.Models; // Adicione esta using

namespace IoTControl.Tests.Unit.Services
{
    // IoTControl.Tests.Unit/Services/DeviceServiceTests.cs
    public class DeviceServiceTests
    {
        [Fact]
        public async Task GetDevices_ReturnsValidList()
        {
            // Arrange
            var mockHttp = new Mock<HttpMessageHandler>();
            mockHttp.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("[\"device1\",\"device2\"]", Encoding.UTF8, "application/json")
                });

            var httpClient = new HttpClient(mockHttp.Object)
            {
                BaseAddress = new Uri("https://2f8e4356-9f20-47af-bb45-d9f86a3b8858.mock.pstmn.io")
            };

            var service = new DeviceService(httpClient);

            // Act
            var result = await service.GetDevices();

            // Assert
            Assert.Equal(2, result.Count());
        }
    }

    // IoTControl.Tests.Unit/Services/CommandServiceTests.cs
    public class CommandServiceTests
    {
        [Fact]
        public async Task ExecuteCommand_ReturnsValidResponse()
        {
            // Arrange
            var mockTelnet = new Mock<ITelnetService>();
            var mockDeviceService = new Mock<IDeviceService>();

            mockDeviceService.Setup(x => x.GetDeviceById(It.IsAny<string>()))
                .ReturnsAsync(new Device { Url = "localhost:23" });

            mockTelnet
              .Setup(x => x.SendCommandAsync(
                  It.IsAny<string>(),
                  It.IsAny<string>(),
                  It.IsAny<string[]>(),
                  It.IsAny<CancellationToken>()   // <- aqui
              ))
              .ReturnsAsync("response");

            var service = new CommandService(mockTelnet.Object, mockDeviceService.Object);

            // Act
            var result = await service.ExecuteCommand("device1", "status", Array.Empty<string>());

            // Assert
            Assert.Equal("response", result);
        }
    }
}
