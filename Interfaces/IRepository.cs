using System.Linq.Expressions;

namespace WMSBackend.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        Task<TEntity?> AddAsync(TEntity entity);

        // GetAsync is implemented at IGenericRepository and IUserRepository
        // This is because IGenericRepository needs int as id
        // IUserRepository needs string as id

        Task<IEnumerable<TEntity>> GetAllAsync(bool isGetRelations);

        Task<IEnumerable<TEntity>> FindAsync(Func<TEntity, bool> predicate, bool isGetRelations);

        Task<bool> UpdateAsync(TEntity entity);
    }
}
