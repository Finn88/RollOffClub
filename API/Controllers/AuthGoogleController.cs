using API.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthGoogleController(IConfiguration configuration) : BaseApiController
    {
        [HttpGet("login")]
        public IActionResult Login()
        {
            var googleAuthUrl = $"https://accounts.google.com/o/oauth2/auth?" +
                                $"client_id={configuration["Google:ClientId"]}&" +
                                $"redirect_uri=http://localhost:5055/api/authgoogle/callback&" +
                                $"response_type=code&" +
                                $"scope=openid%20profile%20email";

            return Redirect(googleAuthUrl);
        }

        [HttpGet("callback")]
        public async Task<IActionResult> Callback(string code)
        {
            if (string.IsNullOrEmpty(code))
                return BadRequest("No code received");

            var tokenRequest = new HttpRequestMessage(HttpMethod.Post, "https://oauth2.googleapis.com/token")
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "code", code },
                { "client_id", $"{configuration["Google:ClientId"]}" },
                { "client_secret", configuration["Google:ClientSecret"] },
                { "redirect_uri", "http://localhost:5055/api/authgoogle/callback" },
                { "grant_type", "authorization_code" }
            })
            };

            var httpClient = new HttpClient();
            var response = await httpClient.SendAsync(tokenRequest);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, responseContent);

            var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(responseContent);

            // Use ID Token to fetch user info
            var userInfoRequest = new HttpRequestMessage(HttpMethod.Get, "https://www.googleapis.com/oauth2/v2/userinfo");
            userInfoRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);

            var userInfoResponse = await httpClient.SendAsync(userInfoRequest);
            var userInfoContent = await userInfoResponse.Content.ReadAsStringAsync();

            if (!userInfoResponse.IsSuccessStatusCode)
                return StatusCode((int)userInfoResponse.StatusCode, userInfoContent);

            var clientRedirectUrl = $"http://localhost:4200?token={tokenResponse.AccessToken}&loginService=google";
            return Redirect(clientRedirectUrl);
        }
    }
}
