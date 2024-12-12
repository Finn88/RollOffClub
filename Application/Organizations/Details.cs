using MediatR;
using Domain.Entities;
using Infrastructure.Data;

namespace Application.Organizations
{
    public class Details
    {
        public class Query : IRequest<Organization>
        {
            public int Id { get; set; }
        }

        public class Handler(DataContext context) : IRequestHandler<Query, Organization>
        {
            public async Task<Organization> Handle(Query request, CancellationToken cancellationToken)
            {
                var repository = RepositoryFactory.Get<Organization>(context);
                return await repository.GetByIdAsync(request.Id);
            }
        }
    }
}
