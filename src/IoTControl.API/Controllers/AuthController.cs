namespace IoTControl.API.Controllers;
using IoTControl.API.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    public AuthController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("token")]
    public IActionResult Token([FromForm] string client_id, [FromForm] string client_secret)
    {
        // Valida o client_id e client_secret de forma fake
        if (client_id != "iot-client" || client_secret != "iot-secret")
            return Unauthorized();

        var token = _tokenService.GenerateToken(client_id);
        return Ok(new { access_token = token, token_type = "Bearer", expires_in = 3600 });
    }
}
