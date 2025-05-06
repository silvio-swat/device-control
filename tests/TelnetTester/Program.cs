using System;
using Microsoft.Extensions.DependencyInjection;
using IoTControl.Application.Services;
using IoTControl.Infrastructure;

var services = new ServiceCollection();
services.AddInfrastructure();
var provider = services.BuildServiceProvider();
var telnet = provider.GetRequiredService<ITelnetService>();

Console.WriteLine("Enviando comando de teste...");
var response = await telnet.SendCommandAsync("localhost:2323", "TESTE", new[] { "param1" });
Console.WriteLine($"Resposta: {response}");
