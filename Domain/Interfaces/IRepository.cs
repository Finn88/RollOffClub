
namespace Infrastructure.Data
{
    public interface IRepository<T> where T : class
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<int> SaveChangesAsync();
    }
}