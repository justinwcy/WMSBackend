using Microsoft.EntityFrameworkCore;
using WMSBackend.Data;
using WMSBackend.Interfaces;
using WMSBackend.Models;

namespace WMSBackend.Repositories
{
    public class ZoneStaffRepository : GenericRepository<ZoneStaff>, IZoneStaffRepository
    {
        public ZoneStaffRepository(DbContext context)
            : base(context) { }

        public DbSet<ZoneStaff> ZoneStaffContext => ((WmsDbContext)Context).ZoneStaffs;

        public override async Task<IEnumerable<ZoneStaff>> GetAllAsync(bool isGetRelations)
        {
            var zoneStaffs = ZoneStaffContext.AsQueryable();
            return await zoneStaffs.ToListAsync();
        }

        public override async Task<ZoneStaff?> GetAsync(Guid id, bool isGetRelations)
        {
            var zoneStaffs = await GetAllAsync(isGetRelations);
            return zoneStaffs.FirstOrDefault(product => product.Id == id);
        }

        public override async Task<IEnumerable<ZoneStaff>> FindAsync(
            Func<ZoneStaff, bool> predicate,
            bool isGetRelations
        )
        {
            var products = await GetAllAsync(isGetRelations);
            return products.Where(predicate);
        }

        public override async Task<bool> UpdateAsync(ZoneStaff zoneStaff)
        {
            var foundZoneStaff = await GetAsync(zoneStaff.Id, false);
            if (foundZoneStaff != null)
            {
                foundZoneStaff.ZoneId = zoneStaff.ZoneId;
                foundZoneStaff.StaffId = zoneStaff.StaffId;
                return true;
            }

            return false;
        }
    }
}
