namespace WMSBackend.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        Task<TEntity?> AddAsync(TEntity entity);

        Task<IEnumerable<TEntity>> GetAllAsync(bool isGetRelations);

        Task<TEntity?> GetAsync(Guid id, bool isGetRelations);

        Task<IEnumerable<TEntity>> FindAsync(Func<TEntity, bool> predicate, bool isGetRelations);

        Task<bool> UpdateAsync(TEntity entity);

        Task<bool> DeleteAsync(Guid id);
    }
}
