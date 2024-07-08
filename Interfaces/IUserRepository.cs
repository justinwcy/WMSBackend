namespace WMSBackend.Interfaces
{
    public interface IUserRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        Task<TEntity?> GetAsync(string id, bool isGetRelations);
        Task<bool> Delete(string id);
    }
}
