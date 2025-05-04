using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace IoTControl.API.Services;
public interface ITokenService
{
    string GenerateToken(string clientId);
}

public class FakeTokenService : ITokenService
{
    private readonly string _key;
    public FakeTokenService(IConfiguration config)
    {
        _key = config["JwtKey"]!;
    }

    public string GenerateToken(string clientId)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, clientId),
            new Claim("role", "user")
        };

        var creds = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
