using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace GatewayAPI.Extensions
{
    public static class AuthenticationExtension
    {
        public static void AddIdentityServices(this WebApplicationBuilder builder) {
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  var tokenKey = builder.Configuration["TokenKey"] ?? throw new Exception("Token not found");
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
                      ValidateIssuer = false,
                      ValidateAudience = false
                  };
              });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AuthenticatedUsers", policy =>
                {
                    policy.RequireAuthenticatedUser();
                });
            });
        }
    }
}
