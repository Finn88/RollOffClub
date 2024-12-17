using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AuthenticationScheme = GatewayAPI.Entities.AuthenticationScheme;

namespace GatewayAPI.Extensions
{
    public static class AuthenticationExtension
    {
        private const string GoogleUrl = "https://accounts.google.com";
        private const string FacebookUrl = "https://www.facebook.com";
        public static void AddIdentityServices(this WebApplicationBuilder builder, AuthenticationScheme scheme)
        {
            var authenticationBuilder = builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                });


            if ((scheme & AuthenticationScheme.Custom) == AuthenticationScheme.Custom) {
                var tokenKey = builder.Configuration["TokenKey"] ?? throw new Exception("Token not found");
                authenticationBuilder.AddCutomJwt(tokenKey);  
            }
            if ((scheme & AuthenticationScheme.Google) == AuthenticationScheme.Google)
            { 
                var clientId = builder.Configuration["Google:ClientId"];
                authenticationBuilder.AddGoogleJwt(clientId);
            }
            if ((scheme & AuthenticationScheme.Facebook) == AuthenticationScheme.Facebook)
            {
                var appId = builder.Configuration["Facebook:AppId"];
                authenticationBuilder.AddFacebookJwt(appId);
            }


            var schemes = scheme.GetSetFlags();
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AuthenticatedUsers", policy =>
                {
                    policy.AddAuthenticationSchemes(schemes);
                    policy.RequireAuthenticatedUser();
                });
            });
        }


        public static void AddCutomJwt(this AuthenticationBuilder authenticationBuilder, string tokenKey)
        {
            authenticationBuilder.AddJwtBearer("Custom", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
        public static void AddGoogleJwt(this AuthenticationBuilder authenticationBuilder, string clientId)
        {
            authenticationBuilder.AddJwtBearer("Google", options =>
            {
                options.Authority = GoogleUrl;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = GoogleUrl,
                    ValidateAudience = true,
                    ValidAudience = clientId,
                    ValidateLifetime = true
                };
            });
        }        
        public static void AddFacebookJwt(this AuthenticationBuilder authenticationBuilder, string appId)
        {
            authenticationBuilder.AddJwtBearer("Facebook", options =>
            {
                options.Authority = FacebookUrl;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = FacebookUrl,
                    ValidateAudience = true,
                    ValidAudience = appId,
                    ValidateLifetime = true
                };
            });
        }
    }

}
