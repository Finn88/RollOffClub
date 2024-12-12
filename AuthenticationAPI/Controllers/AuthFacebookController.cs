using AuthenticationAPI.Services;
using AuthenticationAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthFacebookController : ControllerBase
    {
        private readonly string _baseUrl;
        private readonly IConfiguration _configuration;

        public AuthFacebookController(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseUrl = "localhost:5056";
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            string facebookOAuthUrl = FacebookRequestHelper.GetAuthUrl(_configuration["Facebook:AppId"], _baseUrl);
            return Redirect(facebookOAuthUrl);
        }

        [HttpGet("callback")]
        public async Task<IActionResult> Callback(string? code)
        {
            var token = string.Empty;
            var email = string.Empty;
            using (var client = new HttpClient())
            {
                var tokenUrl = FacebookRequestHelper.GetTokenUrl(_configuration["Facebook:AppId"], _configuration["Facebook:AppSecret"], _baseUrl, code);
                var response = await client.GetAsync(tokenUrl);

                var responseString = await response.Content.ReadAsStringAsync();
                var jsonResponse = JsonConvert.DeserializeObject<JObject>(responseString);
                token = jsonResponse["access_token"]?.ToString();
            }

            using (var client = new HttpClient())
            {
                var emailUrl = FacebookRequestHelper.GetEmailUrl(token);
                var response = await client.GetAsync(emailUrl);

                var responseString = await response.Content.ReadAsStringAsync();
                var jsonResponse = JsonConvert.DeserializeObject<JObject>(responseString);
                email = jsonResponse["email"]?.ToString();
            }

            var jwtToken = await new TokenService().CreateToken(_configuration["TokenKey"], email);
            var clientRedirectUrl = FacebookRequestHelper.GetRedirectUrl(_configuration["ClientUrl"], jwtToken);
            return Redirect(clientRedirectUrl);
        }
    }
}
