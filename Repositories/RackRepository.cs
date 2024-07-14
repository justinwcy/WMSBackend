using Microsoft.EntityFrameworkCore;
using WMSBackend.Data;
using WMSBackend.Interfaces;
using WMSBackend.Models;

namespace WMSBackend.Repositories
{
    public class RackRepository : GenericRepository<Rack>, IRackRepository
    {
        public RackRepository(DbContext context)
            : base(context) { }

        public DbSet<Rack> RackContext => ((WmsDbContext)Context).Racks;

        public override async Task<IEnumerable<Rack>> GetAllAsync(bool isGetRelations)
        {
            var racks = RackContext.AsQueryable();
            if (isGetRelations)
            {
                racks = racks.Include(rack => rack.Zone).Include(rack => rack.Products);
            }

            return await racks.ToListAsync();
        }

        public async Task<List<Rack>> GetAllAsync(bool isGetZone, bool isGetProducts)
        {
            var racks = RackContext.AsQueryable();
            if (isGetZone)
            {
                racks = racks.Include(rack => rack.Zone);
            }

            if (isGetZone)
            {
                racks = racks.Include(rack => rack.Products);
            }

            return await racks.ToListAsync();
        }

        public override async Task<Rack?> GetAsync(Guid id, bool isGetRelations)
        {
            var racks = await GetAllAsync(isGetRelations);
            return racks.FirstOrDefault(rack => rack.Id == id);
        }

        public async Task<Rack?> GetAsync(Guid id, bool isGetZone, bool isGetProducts)
        {
            var racks = await GetAllAsync(isGetZone, isGetProducts);
            return racks.FirstOrDefault(rack => rack.Id == id);
        }

        public override async Task<IEnumerable<Rack>> FindAsync(
            Func<Rack, bool> predicate,
            bool isGetRelations
        )
        {
            var racks = await GetAllAsync(isGetRelations);
            return racks.Where(predicate);
        }

        public override async Task<bool> UpdateAsync(Rack rack)
        {
            var foundRack = await GetAsync(rack.Id, false);
            if (foundRack != null)
            {
                foundRack.Name = rack.Name;
                foundRack.MaxWeight = rack.MaxWeight;
                foundRack.Height = rack.Height;
                foundRack.Width = rack.Width;
                foundRack.Depth = rack.Depth;

                return true;
            }

            return false;
        }
    }
}
