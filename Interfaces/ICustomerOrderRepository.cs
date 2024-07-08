using WMSBackend.Models;

namespace WMSBackend.Interfaces
{
    public interface ICustomerOrderRepository : IGenericRepository<CustomerOrder>
    {
        Task<List<CustomerOrder>> GetAllAsync(
            bool isGetCustomer,
            bool isGetCustomerOrderDetails,
            bool isGetCourier
        );
        Task<CustomerOrder?> GetAsync(
            int id,
            bool isGetCustomer,
            bool isGetCustomerOrderDetails,
            bool isGetCourier
        );
    }
}
