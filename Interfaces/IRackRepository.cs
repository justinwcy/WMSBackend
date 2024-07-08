using WMSBackend.Models;

namespace WMSBackend.Interfaces
{
    public interface IRackRepository : IGenericRepository<Rack>
    {
        Task<List<Rack>> GetAllAsync(bool isGetZone, bool isGetProducts);
        Task<Rack?> GetAsync(int id, bool isGetZone, bool isGetProducts);
    }
}
