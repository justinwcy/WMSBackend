using Microsoft.EntityFrameworkCore;
using WMSBackend.Data;
using WMSBackend.Interfaces;
using WMSBackend.Models;

namespace WMSBackend.Repositories
{
    public class StaffNotificationRepository
        : GenericRepository<StaffNotification>,
            IStaffNotificationRepository
    {
        public StaffNotificationRepository(DbContext context)
            : base(context) { }

        public DbSet<StaffNotification> StaffNotificationContext =>
            ((WmsDbContext)Context).StaffNotifications;

        public override async Task<IEnumerable<StaffNotification>> GetAllAsync(bool isGetRelations)
        {
            var staffNotifications = StaffNotificationContext.AsQueryable();
            if (isGetRelations)
            {
                staffNotifications = staffNotifications.Include(staffNotification =>
                    staffNotification.Staff
                );
            }

            return await staffNotifications.ToListAsync();
        }

        public override async Task<StaffNotification?> GetAsync(int id, bool isGetRelations)
        {
            var staffNotifications = await GetAllAsync(isGetRelations);
            return staffNotifications.FirstOrDefault(staffNotification =>
                staffNotification.Id == id
            );
        }

        public override async Task<IEnumerable<StaffNotification>> FindAsync(
            Func<StaffNotification, bool> predicate,
            bool isGetRelations
        )
        {
            var staffNotifications = await GetAllAsync(isGetRelations);
            return staffNotifications.Where(predicate);
        }

        public override async Task<bool> UpdateAsync(StaffNotification staffNotification)
        {
            var foundStaffNotification = await GetAsync(staffNotification.Id, false);
            if (foundStaffNotification != null)
            {
                foundStaffNotification.Subject = staffNotification.Subject;
                foundStaffNotification.Body = staffNotification.Body;
                foundStaffNotification.IsRead = staffNotification.IsRead;

                return true;
            }

            return false;
        }
    }
}
