using MediatR;
using Infrastructure.Data;
using AutoMapper;

namespace Application.CQRS
{
    public class Edit<T> where T : class
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
            public T Entity { get; set; }
        }

        public class Handler(IRepositoryFactory factory, IMapper mapper) : IRequestHandler<Command>
        {
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var repository = factory.Get<T>();
                var organization = await repository.GetByIdAsync(request.Id);
                mapper.Map(request.Entity, organization);
                await repository.SaveChangesAsync();
            }
        }
    }
}
