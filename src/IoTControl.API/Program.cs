using IoTControl.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

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

app.UseSpa(spa =>
{
    spa.Options.SourcePath = "ClientApp";

    //if (app.Environment.IsDevelopment())
    //{
    //    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
    //}
});

app.Run();
