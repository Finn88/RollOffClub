using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class AuthenticationExtension
    {
        public static void AddAuthAuthenticationGoogle(this WebApplicationBuilder builder)
        {
            var googleAccountsUrl = "https://accounts.google.com";
            builder.Services
                .AddAuthentication(options =>
                      {
                          options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                          options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                      })
                .AddJwtBearer(options =>
                    {
                        options.Authority = googleAccountsUrl;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = googleAccountsUrl,
                            ValidateAudience = true,
                            ValidAudience = $"{builder.Configuration["Google:ClientId"]}",
                            ValidateLifetime = true
                        };
                    });
        }

        public static void AddAuthAuthenticationFacebook(this WebApplicationBuilder builder)
        {

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = FacebookDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                    .AddCookie()
                    .AddFacebook(options =>
                    {
                        options.AppId = builder.Configuration["Facebook:AppId"];
                        options.AppSecret = builder.Configuration["Facebook:AppSecret"];
                        options.CallbackPath = "/authfacebook/callback";
                        options.Scope.Add("email");  
                        options.Fields.Add("name");
                        options.Fields.Add("email");
                        options.SaveTokens = true; 
                    });


        }
    }
}
