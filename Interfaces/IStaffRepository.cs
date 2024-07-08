using WMSBackend.Models;

namespace WMSBackend.Interfaces
{
    public interface IStaffRepository : IUserRepository<Staff>
    {
        Task<List<Staff>> GetAllAsync(bool isGetCompany, bool isGetZones);
        Task<Staff?> GetAsync(string id, bool isGetCompany, bool isGetZones);
    }
}
