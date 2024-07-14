using Microsoft.EntityFrameworkCore;
using WMSBackend.Data;
using WMSBackend.Interfaces;
using WMSBackend.Models;

namespace WMSBackend.Repositories
{
    public class BinRepository : GenericRepository<Bin>, IBinRepository
    {
        public BinRepository(DbContext context)
            : base(context) { }

        public DbSet<Bin> BinContext => ((WmsDbContext)Context).Bins;

        public override async Task<IEnumerable<Bin>> GetAllAsync(bool isGetRelations)
        {
            var bins = BinContext.AsQueryable();
            if (isGetRelations)
            {
                bins = bins.Include(bin => bin.CustomerOrders);
            }

            return await bins.ToListAsync();
        }

        public override async Task<Bin?> GetAsync(Guid id, bool isGetRelations)
        {
            var bins = await GetAllAsync(isGetRelations);
            return bins.FirstOrDefault(bin => bin.Id == id);
        }

        public override async Task<IEnumerable<Bin>> FindAsync(
            Func<Bin, bool> predicate,
            bool isGetRelations
        )
        {
            var bins = await GetAllAsync(isGetRelations);
            return bins.Where(predicate);
        }

        public override async Task<bool> UpdateAsync(Bin bin)
        {
            var foundBin = await GetAsync(bin.Id, false);
            if (foundBin != null)
            {
                foundBin.Name = bin.Name;
                return true;
            }
            return false;
        }
    }
}
