using WMSBackend.Models;

namespace WMSBackend.Interfaces
{
    public interface ICustomerOrderRepository : IRepository<CustomerOrder>
    {
        Task<List<CustomerOrder>> GetAllAsync(
            bool isGetCustomer,
            bool isGetCustomerOrderDetails,
            bool isGetCourier,
            bool isGetBin
        );
        Task<CustomerOrder?> GetAsync(
            Guid id,
            bool isGetCustomer,
            bool isGetCustomerOrderDetails,
            bool isGetCourier,
            bool isGetBin
        );
    }
}
