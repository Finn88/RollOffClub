
namespace AuthFacebookTokenAPI.Utils
{
    public static class FacebookRequestHelper
    {
        private const string FacebookAuthUrl = "https://www.facebook.com/v12.0/dialog/oauth";
        private const string FacebookTokenUrl = "https://graph.facebook.com/v12.0/oauth/access_token";
        private const string FacebookEmailUrl = "https://graph.facebook.com/v12.0/me?fields=email&access_token=";

        public static string GetAuthUrl(string appId, string baseUrl)
        {
            return $"{FacebookAuthUrl}?client_id={appId}&redirect_uri={GetRedirectBaseUrl(baseUrl)}&scope=email,public_profile";
        }

        public static string GetTokenUrl(string appId, string appSecret, string baseUrl, string code)
        {
            return $"{FacebookTokenUrl}?client_id={appId}&redirect_uri={GetRedirectBaseUrl(baseUrl)}&client_secret={appSecret}&code={code}";
        }
        public static string GetEmailUrl(string token)
        {
            return $"{FacebookEmailUrl}{token}";
        }

        public static string GetRedirectUrl(string clientUrl, string token)
        {
            return $"http://{clientUrl}?token={token}&loginService=facebook";
        }

        private static string GetRedirectBaseUrl(string baseUrl)
        {
            return $"http://{baseUrl}/api/authfacebook/callback";
        }
    }
}
