using AuthGoogleTokenAPI.Utils;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AuthGoogleTokenAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthGoogleController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public AuthGoogleController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            var googleAuthUrl = GoogleRequestHelper.GetAuthUrl(_configuration["Google:ClientId"], _configuration["ClientUrl"]);
            return Redirect(googleAuthUrl);
        }

        [HttpPost("token")]
        public async Task<IActionResult> Callback([FromBody] string code)
        {
            if (string.IsNullOrEmpty(code))
                return BadRequest("No code received");

            var tokenUrl = GoogleRequestHelper.GetTokenUrl();
            var tokenRequest = new HttpRequestMessage(HttpMethod.Post, tokenUrl)
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "code", code },
                    { "client_id", $"{_configuration["Google:ClientId"]}" },
                    { "client_secret", _configuration["Google:ClientSecret"] },
                    { "redirect_uri", $"http://{_configuration["ClientUrl"]}?authService=google" },
                    { "grant_type", "authorization_code" }
                })
            };

            var httpClient = new HttpClient();
            var response = await httpClient.SendAsync(tokenRequest);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, responseContent);

            var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);
            var jwtToken = tokenResponse?.IdToken ?? string.Empty;

            Response.Cookies.Append("AuthToken", jwtToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false, 
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(1)
            });
            return Ok();
        }
    }
}
