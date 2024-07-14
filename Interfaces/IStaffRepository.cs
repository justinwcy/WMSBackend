using WMSBackend.Models;

namespace WMSBackend.Interfaces
{
    public interface IStaffRepository : IRepository<Staff>
    {
        Task<List<Staff>> GetAllAsync(bool isGetCompany, bool isGetZones, bool isGetNotifications);
        Task<Staff?> GetAsync(
            string id,
            bool isGetCompany,
            bool isGetZones,
            bool isGetNotifications
        );
    }
}
