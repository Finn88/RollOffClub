using AuthFacebookTokenAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Domain.Entities;

namespace AuthFacebookTokenAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthFacebookController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthFacebookController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            string facebookOAuthUrl = FacebookRequestHelper.GetAuthUrl(_configuration["Facebook:AppId"], _configuration["ClientUrl"]);
            return Redirect(facebookOAuthUrl);
        }

        [HttpPost("token")]
        public async Task<IActionResult> Callback([FromBody] string code)
        {
            var tokenUrl = FacebookRequestHelper.GetTokenUrl();
            var tokenRequest = new HttpRequestMessage(HttpMethod.Post, tokenUrl)
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "code", code },
                    { "client_id", $"{_configuration["Facebook:AppId"]}" },
                    { "client_secret", _configuration["Facebook:AppSecret"] },
                    { "redirect_uri",FacebookRequestHelper.GetRedirectBaseUrl(_configuration["ClientUrl"]) }
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
