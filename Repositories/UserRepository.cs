using Microsoft.EntityFrameworkCore;
using WMSBackend.Interfaces;

namespace WMSBackend.Repositories
{
    public abstract class UserRepository<TEntity> : IUserRepository<TEntity>
        where TEntity : class
    {
        protected readonly DbContext Context;

        private DbSet<TEntity> _dbSet;

        public UserRepository(DbContext context)
        {
            Context = context;
            _dbSet = Context.Set<TEntity>();
        }

        public abstract Task<TEntity?> GetAsync(string id, bool isGetRelations);

        public abstract Task<IEnumerable<TEntity>> GetAllAsync(bool isGetRelations);

        public abstract Task<IEnumerable<TEntity>> FindAsync(
            Func<TEntity, bool> predicate,
            bool isGetRelations
        );

        public abstract Task<bool> UpdateAsync(TEntity entity);

        public async Task<TEntity?> AddAsync(TEntity entity)
        {
            var addedEntity = await _dbSet.AddAsync(entity);
            if (addedEntity != null)
            {
                return addedEntity.Entity;
            }

            return null;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var foundEntity = await GetAsync(id, false);
            if (foundEntity != null)
            {
                _dbSet.Remove(foundEntity);
                return true;
            }
            return false;
        }
    }
}
