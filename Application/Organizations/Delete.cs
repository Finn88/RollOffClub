using MediatR;
using Domain.Entities;
using Infrastructure.Data;

namespace Application.Organizations
{
    public class Delete
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
        }

        public class Handler(DataContext context) : IRequestHandler<Command>
        {
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var repository = RepositoryFactory.Get<Organization>(context);
                var organization = await repository.GetByIdAsync(request.Id);
                repository.Delete(organization);
                await context.SaveChangesAsync();
            }
        }
    }
}
