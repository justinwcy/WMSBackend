using Microsoft.EntityFrameworkCore;
using WMSBackend.Data;
using WMSBackend.Interfaces;
using WMSBackend.Models;

namespace WMSBackend.Repositories
{
    public class StaffRepository : UserRepository<Staff>, IStaffRepository
    {
        public StaffRepository(DbContext context)
            : base(context) { }

        public DbSet<Staff> StaffContext => ((WmsDbContext)Context).Staffs;

        public override async Task<IEnumerable<Staff>> GetAllAsync(bool isGetRelations)
        {
            var staffs = StaffContext.AsQueryable();
            if (isGetRelations)
            {
                staffs = staffs
                    .Include(staff => staff.Company)
                    .Include(staff => staff.Zones)
                    .Include(staff => staff.StaffNotifications);
            }

            return await staffs.ToListAsync();
        }

        public async Task<List<Staff>> GetAllAsync(
            bool isGetCompany,
            bool isGetZones,
            bool isGetStaffNotifications
        )
        {
            var staffs = StaffContext.AsQueryable();
            if (isGetCompany)
            {
                staffs = staffs.Include(staff => staff.Company);
            }

            if (isGetZones)
            {
                staffs = staffs.Include(staff => staff.Zones);
            }

            if (isGetStaffNotifications)
            {
                staffs = staffs.Include(staff => staff.StaffNotifications);
            }

            return await staffs.ToListAsync();
        }

        public override async Task<Staff?> GetAsync(string id, bool isGetRelations)
        {
            var staffs = await GetAllAsync(isGetRelations);
            return staffs.FirstOrDefault(staff => staff.Id == id);
        }

        public async Task<Staff?> GetAsync(
            string id,
            bool isGetCompany,
            bool isGetZones,
            bool isGetStaffNotifications
        )
        {
            var staffs = await GetAllAsync(isGetCompany, isGetZones, isGetStaffNotifications);
            return staffs.FirstOrDefault(staff => staff.Id == id);
        }

        public override async Task<IEnumerable<Staff>> FindAsync(
            Func<Staff, bool> predicate,
            bool isGetRelations
        )
        {
            var staffs = await GetAllAsync(isGetRelations);
            return staffs.Where(predicate);
        }

        public override async Task<bool> UpdateAsync(Staff staff)
        {
            var foundStaff = await GetAsync(staff.Id, false);
            if (foundStaff != null)
            {
                foundStaff.FirstName = staff.FirstName;
                foundStaff.LastName = staff.LastName;

                return true;
            }

            return false;
        }
    }
}
