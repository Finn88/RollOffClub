using MediatR;
using Domain.Entities;
using Infrastructure.Data;

namespace Application.Organizations
{
    public class Create
    {
        public class Command : IRequest
        {
            public Organization Organization { get; set; }
        }

        public class Handler(DataContext context) : IRequestHandler<Command>
        {
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                RepositoryFactory.Get<Organization>(context).Create(request.Organization);
                await context.SaveChangesAsync();
            }
        }
    }
}
