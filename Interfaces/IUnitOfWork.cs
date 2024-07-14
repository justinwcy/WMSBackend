namespace WMSBackend.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBinRepository BinRepository { get; }
        ICompanyRepository CompanyRepository { get; }
        ICourierRepository CourierRepository { get; }
        ICustomerOrderDetailRepository CustomerOrderDetailRepository { get; }
        ICustomerOrderRepository CustomerOrderRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IIncomingOrderProductRepository IncomingOrderProductRepository { get; }
        IIncomingOrderRepository IncomingOrderRepository { get; }
        IInventoryRepository InventoryRepository { get; }
        IProductRepository ProductRepository { get; }
        IRackRepository RackRepository { get; }
        IRefundOrderProductRepository RefundOrderProductRepository { get; }
        IRefundOrderRepository RefundOrderRepository { get; }
        IShopRepository ShopRepository { get; }
        IStaffRepository StaffRepository { get; }
        IStaffNotificationRepository StaffNotificationRepository { get; }
        IVendorRepository VendorRepository { get; }
        IWarehouseRepository WarehouseRepository { get; }
        IZoneRepository ZoneRepository { get; }

        Task<int> CommitAsync();
    }
}
