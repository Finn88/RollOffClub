namespace Infrastructure.Data
{
    public interface IRepositoryFactory
    {
        IRepository<T> Get<T>() where T : class;
    }
}