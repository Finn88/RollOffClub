using AuthGoogleTokenAPI.Services;
using AuthGoogleTokenAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text.Json;

namespace AuthGoogleTokenAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthGoogleController : ControllerBase
    {
        private readonly string _baseUrl;

        private readonly IConfiguration _configuration;
        public AuthGoogleController(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseUrl = _configuration["HostUrl"];
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            var googleAuthUrl = GoogleRequestHelper.GetAuthUrl(_configuration["Google:ClientId"], _baseUrl);
            return Redirect(googleAuthUrl);
        }

        [HttpGet("callback")]
        public async Task<IActionResult> Callback(string code)
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
                    { "redirect_uri", $"{GoogleRequestHelper.GetRedirectBaseUrl(_baseUrl)}" },
                    { "grant_type", "authorization_code" }
                })
            };

            var httpClient = new HttpClient();
            var response = await httpClient.SendAsync(tokenRequest);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, responseContent);

            var tokenResponse = JsonConvert.DeserializeObject<JObject>(responseContent);

            var userInfoRequest = new HttpRequestMessage(HttpMethod.Get, GoogleRequestHelper.GetUserInfoUrl());
            userInfoRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse["access_token"]?.ToString());

            var userInfoResponse = await httpClient.SendAsync(userInfoRequest);
            var userInfoContent = await userInfoResponse.Content.ReadAsStringAsync();
            var userInfoContentResponse = JsonConvert.DeserializeObject<JObject>(userInfoContent);
            var email = userInfoContentResponse["email"]?.ToString();

            if (!userInfoResponse.IsSuccessStatusCode)
                return StatusCode((int)userInfoResponse.StatusCode, userInfoContent);

            var jwtToken = await new TokenService().CreateToken(_configuration["TokenKey"], email);
            var clientRedirectUrl = GoogleRequestHelper.GetRedirectUrl(_configuration["ClientUrl"], jwtToken);
            return Redirect(clientRedirectUrl);
        }
    }
}
