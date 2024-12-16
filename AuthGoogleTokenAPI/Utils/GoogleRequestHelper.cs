
namespace AuthGoogleTokenAPI.Utils
{
    public static class GoogleRequestHelper
    {
        private const string GoogleAuthUrl = "https://accounts.google.com/o/oauth2/auth";
        private const string GoogleTokenUrl = "https://oauth2.googleapis.com/token";

        public static string GetAuthUrl(string clientId, string clientUrl)
        {
            return $"{GoogleAuthUrl}?" +
                                $"client_id={clientId}&" +
                                $"redirect_uri=http://{clientUrl}?authService=google&" +
                                $"response_type=code&" +
                                $"scope=openid%20profile%20email";
        }

        public static string GetTokenUrl()
        {
            return GoogleTokenUrl;
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
