using MediatR;
using Domain.Entities;
using Infrastructure.Data;

namespace Application.CQRS
{
    public class Details<T> where T : class
    {
        public class Query : IRequest<T>
        {
            public int Id { get; set; }
        }

        public class Handler(IRepositoryFactory factory) : IRequestHandler<Query, T>
        {
            public async Task<T> Handle(Query request, CancellationToken cancellationToken)
            {
                var repository = factory.Get<T>();
                return await repository.GetByIdAsync(request.Id);
            }
        }
    }
}
