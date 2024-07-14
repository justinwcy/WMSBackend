using WMSBackend.Models;

namespace WMSBackend.Interfaces
{
    public interface IIncomingOrderRepository : IRepository<IncomingOrder>
    {
        Task<IncomingOrder?> GetAsync(Guid id, bool isGetProducts, bool isGetVendor);

        Task<List<IncomingOrder>> GetAllAsync(bool isGetProducts, bool isGetVendor);
    }
}
