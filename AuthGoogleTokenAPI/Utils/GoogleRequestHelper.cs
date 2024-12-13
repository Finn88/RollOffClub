using Microsoft.AspNetCore.Authentication;

namespace AuthGoogleTokenAPI.Utils
{
    public static class GoogleRequestHelper
    {
        private const string GoogleAuthUrl = "https://accounts.google.com/o/oauth2/auth";
        private const string GoogleTokenUrl = "https://oauth2.googleapis.com/token";
        private const string GoogleUserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

        public static string GetAuthUrl(string clientId, string baseUrl)
        {
            return $"{GoogleAuthUrl}?" +
                                $"client_id={clientId}&" +
                                $"redirect_uri={GetRedirectBaseUrl(baseUrl)}&" +
                                $"response_type=code&" +
                                $"scope=openid%20profile%20email";
        }

        public static string GetTokenUrl()
        {
            return GoogleTokenUrl;
        }

        public static string GetUserInfoUrl()
        {
            return GoogleUserInfoUrl;
        }

        public static string GetRedirectUrl(string clientUrl, string token)
        {
            return $"http://{clientUrl}?token={token}&loginService=google";
        }

        public static string GetRedirectBaseUrl(string baseUrl)
        {
            return $"http://{baseUrl}/api/authgoogle/callback";
        }
    }
}
