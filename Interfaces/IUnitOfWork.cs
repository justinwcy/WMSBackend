namespace WMSBackend.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICompanyRepository CompanyRepository { get; }
        ICourierRepository CourierRepository { get; }
        ICustomerOrderDetailRepository CustomerOrderDetailRepository { get; }
        ICustomerOrderRepository CustomerOrderRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IIncomingOrderRepository IncomingOrderRepository { get; }
        IInventoryRepository InventoryRepository { get; }
        IProductRepository ProductRepository { get; }
        IRackRepository RackRepository { get; }
        IRefundOrderRepository RefundOrderRepository { get; }
        IShopRepository ShopRepository { get; }
        IStaffRepository StaffRepository { get; }
        IWarehouseRepository WarehouseRepository { get; }
        IZoneRepository ZoneRepository { get; }

        Task<int> CommitAsync();
    }
}
