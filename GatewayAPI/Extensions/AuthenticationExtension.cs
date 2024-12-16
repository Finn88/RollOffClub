using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace GatewayAPI.Extensions
{
    public static class AuthenticationExtension
    {
        public static void AddIdentityServices(this WebApplicationBuilder builder) {
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer("Custom", options =>
              {
                  var tokenKey = builder.Configuration["TokenKey"] ?? throw new Exception("Token not found");
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
                      ValidateIssuer = false,
                      ValidateAudience = false
                  };
              })
              .AddJwtBearer("Google", options =>
              {
                  options.Authority = "https://accounts.google.com";
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidIssuer = "https://accounts.google.com",
                      ValidateAudience = true,
                      ValidAudience = builder.Configuration["Google:ClientId"],
                      ValidateLifetime = true
                  };
              })
            .AddJwtBearer("Facebook", options =>
            {
                options.Authority = "https://www.facebook.com";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "https://www.facebook.com",
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Facebook:AppId"],
                    ValidateLifetime = true
                };
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AuthenticatedUsers", policy =>
                {
                    policy.AddAuthenticationSchemes("Custom", "Google", "Facebook");
                    policy.RequireAuthenticatedUser();
                });
            });
        }
    }
}
