using WMSBackend.Models;

namespace WMSBackend.Interfaces
{
    public interface IRackRepository : IRepository<Rack>
    {
        Task<List<Rack>> GetAllAsync(bool isGetZone, bool isGetProducts);
        Task<Rack?> GetAsync(Guid id, bool isGetZone, bool isGetProducts);
    }
}
