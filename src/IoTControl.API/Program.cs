using IoTControl.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configura��o OAuth2 Fake (JWT Bearer)
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

// Registro do servi�o de gera��o de token fake
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

// Habilita autentica��o e autoriza��o JWT
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
