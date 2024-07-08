using WMSBackend.Models;

namespace WMSBackend.Interfaces
{
    public interface IWarehouseRepository : IGenericRepository<Warehouse>
    {
        Task<List<Warehouse>> GetAllAsync(bool isGetCompany, bool isGetZones);
        Task<Warehouse?> GetAsync(int id, bool isGetCompany, bool isGetZones);
    }
}
