using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthFacebookController(IConfiguration configuration) : BaseApiController
    {
        string redirectUri = "http://localhost:5055/api/authfacebook/callback";

        [HttpGet("login")]
        public IActionResult Login()
        {
            string facebookOAuthUrl = $"https://www.facebook.com/v12.0/dialog/oauth?client_id={configuration["Facebook:AppId"]}&redirect_uri={redirectUri}&scope=email,public_profile";
            return Redirect(facebookOAuthUrl);
        }

        [HttpGet("callback")]
        public async Task<IActionResult> Callback(string? code)
        {
            var token = string.Empty;
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"https://graph.facebook.com/v12.0/oauth/access_token?client_id={configuration["Facebook:AppId"]}&redirect_uri={redirectUri}&client_secret={configuration["Facebook:AppSecret"]}&code={code}");

                var responseString = await response.Content.ReadAsStringAsync();
                var jsonResponse = JsonConvert.DeserializeObject<JObject>(responseString);
                token = jsonResponse["access_token"]?.ToString();
            }

            var clientRedirectUrl = $"http://localhost:4200?token={token}&loginService=facebook";
            return Redirect(clientRedirectUrl);
        }
    }
}
