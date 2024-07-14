using WMSBackend.Models;

namespace WMSBackend.Interfaces
{
    public interface IWarehouseRepository : IRepository<Warehouse>
    {
        Task<List<Warehouse>> GetAllAsync(bool isGetCompany, bool isGetZones);
        Task<Warehouse?> GetAsync(Guid id, bool isGetCompany, bool isGetZones);
    }
}
