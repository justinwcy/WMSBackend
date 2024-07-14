using Microsoft.EntityFrameworkCore;
using WMSBackend.Data;
using WMSBackend.Interfaces;
using WMSBackend.Models;

namespace WMSBackend.Repositories
{
    public class ZoneRepository : GenericRepository<Zone>, IZoneRepository
    {
        public ZoneRepository(DbContext context)
            : base(context) { }

        public DbSet<Zone> ZoneContext => ((WmsDbContext)Context).Zones;

        public override async Task<IEnumerable<Zone>> GetAllAsync(bool isGetRelations)
        {
            var zones = ZoneContext.AsQueryable();
            if (isGetRelations)
            {
                zones = zones
                    .Include(zone => zone.Warehouse)
                    .Include(zone => zone.Staffs)
                    .Include(zone => zone.Racks);
            }

            return await zones.ToListAsync();
        }

        public async Task<List<Zone>> GetAllAsync(
            bool isGetWarehouse,
            bool isGetStaffs,
            bool isGetRacks
        )
        {
            var zones = ZoneContext.AsQueryable();
            if (isGetWarehouse)
            {
                zones = zones.Include(zone => zone.Warehouse);
            }
            if (isGetStaffs)
            {
                zones = zones.Include(zone => zone.Staffs);
            }
            if (isGetRacks)
            {
                zones = zones.Include(zone => zone.Racks);
            }

            return await zones.ToListAsync();
        }

        public override async Task<Zone?> GetAsync(Guid id, bool isGetRelations)
        {
            var zones = await GetAllAsync(isGetRelations);
            return zones.FirstOrDefault(zone => zone.Id == id);
        }

        public async Task<Zone?> GetAsync(
            Guid id,
            bool isGetWarehouse,
            bool isGetStaffs,
            bool isGetRacks
        )
        {
            var zones = await GetAllAsync(isGetWarehouse, isGetStaffs, isGetRacks);
            return zones.FirstOrDefault(zone => zone.Id == id);
        }

        public override async Task<IEnumerable<Zone>> FindAsync(
            Func<Zone, bool> predicate,
            bool isGetRelations
        )
        {
            var zones = await GetAllAsync(isGetRelations);
            return zones.Where(predicate);
        }

        public override async Task<bool> UpdateAsync(Zone zone)
        {
            var foundZone = await GetAsync(zone.Id, false);
            if (foundZone != null)
            {
                foundZone.Name = zone.Name;

                return true;
            }

            return false;
        }
    }
}
