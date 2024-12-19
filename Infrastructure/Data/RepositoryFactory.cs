namespace Infrastructure.Data
{
    public class RepositoryFactory(DataContext context) : IRepositoryFactory
    {
        public IRepository<T> Get<T>() where T : class 
        {
            return new Repository<T>(context);
        }
    }
}
