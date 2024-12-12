using MediatR;
using Domain.Entities;
using Infrastructure.Data;
using AutoMapper;

namespace Application.Organizations
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Organization Organization { get; set; }
        }

        public class Handler(DataContext context, IMapper mapper) : IRequestHandler<Command>
        {
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var repository = RepositoryFactory.Get<Organization>(context);
                var organization = await repository.GetByIdAsync(request.Organization.Id);
                mapper.Map(request.Organization, organization);
                await context.SaveChangesAsync();
            }
        }
    }
}
