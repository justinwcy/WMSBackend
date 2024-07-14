using WMSBackend.Models;

namespace WMSBackend.Interfaces
{
    public interface IZoneRepository : IRepository<Zone>
    {
        Task<List<Zone>> GetAllAsync(bool isGetWarehouse, bool isGetStaffs, bool isGetRacks);
        Task<Zone?> GetAsync(Guid id, bool isGetWarehouse, bool isGetStaffs, bool isGetRacks);
    }
}
