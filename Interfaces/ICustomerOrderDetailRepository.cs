using WMSBackend.Models;

namespace WMSBackend.Interfaces
{
    public interface ICustomerOrderDetailRepository : IRepository<CustomerOrderDetail>
    {
        Task<List<CustomerOrderDetail>> GetAllAsync(bool isGetCustomerOrder, bool isGetProduct);
        Task<CustomerOrderDetail?> GetAsync(Guid id, bool isGetCustomerOrder, bool isGetProduct);
    }
}
