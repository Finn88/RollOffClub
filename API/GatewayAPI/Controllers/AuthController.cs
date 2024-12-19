
using Microsoft.AspNetCore.Mvc;


namespace GatewayAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Append("AuthToken", string.Empty, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(-1) 
            });

            return Ok(new { Message = "Logged out successfully" });
        }
    }
}
