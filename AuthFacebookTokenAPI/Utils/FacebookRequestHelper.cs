
namespace AuthFacebookTokenAPI.Utils
{
    public static class FacebookRequestHelper
    {
        private const string FacebookAuthUrl = "https://www.facebook.com/v12.0/dialog/oauth";
        private const string FacebookTokenUrl = "https://graph.facebook.com/v12.0/oauth/access_token";

        public static string GetAuthUrl(string appId, string clientUrl)
        {
            return $"{FacebookAuthUrl}?client_id={appId}&redirect_uri={GetRedirectBaseUrl(clientUrl)}&scope=email,public_profile,openid";
        }

        public static string GetTokenUrl() {
            return FacebookTokenUrl;
        }


        public static string GetRedirectBaseUrl(string clientUrl)
        {
            return $"http://{clientUrl}/?authService=facebook";
        }
    }
}
