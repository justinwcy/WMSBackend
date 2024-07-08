using System.Linq.Expressions;

namespace WMSBackend.Interfaces
{
    public interface IGenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        Task<TEntity?> GetAsync(int id, bool isGetRelations);
        Task<bool> Delete(int id);
    }
}
