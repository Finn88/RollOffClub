using MediatR;
using Infrastructure.Data;

namespace Application.CQRS
{
    public class Create<T> where T : class
    {
        public class Command : IRequest
        {
            public T Entity { get; set; }
        }

        public class Handler(IRepositoryFactory factory) : IRequestHandler<Command>
        {
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var repository = factory.Get<T>();
                repository.Create(request.Entity);
                await repository.SaveChangesAsync();
            }
        }
    }
}
