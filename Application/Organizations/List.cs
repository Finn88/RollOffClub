using MediatR;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace Application.Organizations
{
    public class List
    {
        public class Query : IRequest<List<Organization>> { }
        public class Handler(DataContext context, ILogger<List> logger) : IRequestHandler<Query, IEnumerable<Organization>>
        {
            public async Task<IEnumerable<Organization>> Handle(Query request, CancellationToken cancellationToken)
            {
                var repository = RepositoryFactory.Get<Organization>(context);
                return await repository.GetAllAsync();           
            }
        }
    }
}
