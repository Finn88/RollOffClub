
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace AuthGoogleTokenAPI.Services
{
    public class TokenService
    {
        public async Task<string> CreateToken(string tokenKey, string email)
        {;
            if (tokenKey.Length < 64) throw new Exception("Token is invalid by length");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email)
            };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
