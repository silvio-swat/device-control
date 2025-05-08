using System;
using System.Net.Http.Headers;
using System.Text;
using IoTControl.API.Services;
using IoTControl.Application.Services;
using IoTControl.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using IoTControl.Infrastructure;  // para AddInfrastructure

var builder = WebApplication.CreateBuilder(args);

// 1) Registra o DeviceService com HttpClient configurado
builder.Services
    .AddHttpClient<IDeviceService, DeviceService>(client =>
    {
        client.BaseAddress = new Uri("https://2f8e4356-9f20-47af-bb45-d9f86a3b8858.mock.pstmn.io");
        client.DefaultRequestHeaders
              .Accept
              .Add(new MediaTypeWithQualityHeaderValue("application/json"));
    });

// 2) Registra os outros serviços que seu projeto usa:
builder.Services.AddScoped<ITelnetService, TelnetService>();
builder.Services.AddScoped<ICommandService, CommandService>();

// Configuração OAuth2 Fake (JWT Bearer)
var jwtKey = builder.Configuration["JwtKey"] ?? "chave-super-secreta-para-fake-oauth2";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });
builder.Services.AddAuthorization();

// Registro do serviço de geração de token fake
builder.Services.AddSingleton<ITokenService, FakeTokenService>();


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "ClientApp/dist/ClientApp";
});

// 1) Registra serviços de Infraestrutura (inclui TcpClientWrapper)
builder.Services.AddInfrastructure();

// 1) Registra os Controllers
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Roteamento
app.UseRouting();

// Habilita autenticação e autorização JWT
app.UseAuthentication();
app.UseAuthorization();

// Mapeamento de endpoints
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

// Antes de chamar UseSpa:
app.MapWhen(
    ctx => !ctx.Request.Path.StartsWithSegments("/auth")
         && !ctx.Request.Path.StartsWithSegments("/api"),
    spaApp =>
    {
        spaApp.UseSpa(spa =>
        {
            spa.Options.SourcePath = "ClientApp";
            if (app.Environment.IsDevelopment())
            {
                spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
            }
        });
    }
);

app.Run();
