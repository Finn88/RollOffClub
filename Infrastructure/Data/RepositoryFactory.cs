namespace Infrastructure.Data
{
    public static class RepositoryFactory
    {
        public static Repository<T> Get<T>(DataContext context) where T : class {
            return new Repository<T>(context);
        }
    }
}
