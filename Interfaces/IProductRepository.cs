using WMSBackend.Models;

namespace WMSBackend.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product?> GetAsync(
            int id,
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
