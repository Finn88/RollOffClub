using Application.CQRS;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationsController : BaseApiController
    {
        [HttpGet()]
        public async Task<ActionResult> Get()
        {
            var items = await Mediator.Send(new GetAll<Organization>.Query { });
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var item = await Mediator.Send(new Get<Organization>.Query { Id = id });
            return Ok(item);
        }

        [HttpPost()]
        public async Task<ActionResult> Create(Organization organization)
        {
            await Mediator.Send(new Create<Organization>.Command { Entity = organization });
            return CreatedAtAction(nameof(Get), new { id = organization.Id }, organization);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Organization organization)
        {
            await Mediator.Send(new Edit<Organization>.Command { Id = organization.Id, Entity = organization });
            return Ok(organization);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new Edit<Organization>.Command { Id = id });
            return Ok();
        }
    }
}
