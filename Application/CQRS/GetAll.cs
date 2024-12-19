using MediatR;
using Infrastructure.Data;
namespace Application.CQRS
{
    public class GetAll<T> where T : class
    {
        public class Query : IRequest<IEnumerable<T>> { }

        public class Handler(IRepositoryFactory factory) : IRequestHandler<Query, IEnumerable<T>>
        {
            public async Task<IEnumerable<T>> Handle(Query request, CancellationToken cancellationToken)
            {
                var repository = factory.Get<T>();
                return await repository.GetAllAsync();           
            }
        }
    }
}
