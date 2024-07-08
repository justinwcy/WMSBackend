using WMSBackend.Models;

namespace WMSBackend.Interfaces
{
    public interface IZoneRepository : IGenericRepository<Zone>
    {
        Task<List<Zone>> GetAllAsync(bool isGetWarehouse, bool isGetStaffs, bool isGetRacks);
        Task<Zone?> GetAsync(int id, bool isGetWarehouse, bool isGetStaffs, bool isGetRacks);
    }
}
