using Application.Organizations;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationsController : BaseApiController
    {
        [HttpPost("register")]
        public async Task<ActionResult> Register(Organization organization)
        {
            await Mediator.Send(new Create.Command { Organization = organization });
            return Ok();
        }

        [HttpGet("data")]
        public IActionResult GetData()
        {
            return Ok("This is protected data.");
        }
    }
}
