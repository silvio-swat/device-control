using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoTControl.API.Controllers
{
    [ApiController]
    [Route("ciotd-proxy")]
    [Authorize] // Integra com seu OAuth2 fake
    public class CiotdProxyController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private const string MockApiUrl = "https://seu-postman-mock-url";

        public CiotdProxyController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            // Configurar Basic Auth conforme necessário
            var authValue = Convert.ToBase64String(Encoding.UTF8.GetBytes("user:pass"));
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", authValue);
        }

        [HttpGet("devices")]
        public async Task<IActionResult> GetDevices()
        {
            var response = await _httpClient.GetAsync($"{MockApiUrl}/device");
            return await HandleResponse(response);
        }

        [HttpGet("devices/{id}")]
        public async Task<IActionResult> GetDevice(string id)
        {
            var response = await _httpClient.GetAsync($"{MockApiUrl}/device/{id}");
            return await HandleResponse(response);
        }

        private async Task<IActionResult> HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }
    }
}
