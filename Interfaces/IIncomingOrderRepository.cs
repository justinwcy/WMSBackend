using WMSBackend.Models;

namespace WMSBackend.Interfaces
{
    public interface IIncomingOrderRepository : IGenericRepository<IncomingOrder>
    {
        Task<IncomingOrder?> GetAsync(int id, bool isGetProducts, bool isGetVendor);

        Task<List<IncomingOrder>> GetAllAsync(bool isGetProducts, bool isGetVendor);
    }
}
