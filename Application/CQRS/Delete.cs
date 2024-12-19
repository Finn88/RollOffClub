using MediatR;
using Infrastructure.Data;

namespace Application.CQRS
{
    public class Delete<T> where T : class
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
        }

        public class Handler(IRepositoryFactory factory) : IRequestHandler<Command>
        {
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var repository = factory.Get<T>();
                var organization = await repository.GetByIdAsync(request.Id);
                repository.Delete(organization);
                await repository.SaveChangesAsync();
            }
        }
    }
}
