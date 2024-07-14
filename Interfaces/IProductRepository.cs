using WMSBackend.Models;

namespace WMSBackend.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product?> GetAsync(
            Guid id,
            bool isGetIncomingOrders,
            bool isGetRefundOrders,
            bool isGetShops,
            bool isGetRacks,
            bool isGetCurrentInventory,
            bool isGetCustomerOrderDetails
        );

        Task<List<Product>> GetAllAsync(
            bool isGetIncomingOrders,
            bool isGetRefundOrders,
            bool isGetShops,
            bool isGetRacks,
            bool isGetCurrentInventory,
            bool isGetCustomerOrderDetails
        );
    }
}
