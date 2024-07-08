using WMSBackend.Models;

namespace WMSBackend.Interfaces
{
    public interface ICustomerOrderDetailRepository : IGenericRepository<CustomerOrderDetail>
    {
        Task<List<CustomerOrderDetail>> GetAllAsync(bool isGetCustomerOrder, bool isGetProduct);
        Task<CustomerOrderDetail?> GetAsync(int id, bool isGetCustomerOrder, bool isGetProduct);
    }
}
