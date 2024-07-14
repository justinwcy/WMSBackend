using WMSBackend.Data;
using WMSBackend.Interfaces;

namespace WMSBackend.Repositories
{
    public class UnitOfWork(WmsDbContext wmsDbContext) : IUnitOfWork
    {
        public IBinRepository BinRepository { get; private set; } = new BinRepository(wmsDbContext);
        public ICompanyRepository CompanyRepository { get; private set; } =
            new CompanyRepository(wmsDbContext);

        public ICourierRepository CourierRepository { get; private set; } =
            new CourierRepository(wmsDbContext);

        public ICustomerOrderDetailRepository CustomerOrderDetailRepository { get; private set; } =
            new CustomerOrderDetailRepository(wmsDbContext);

        public ICustomerOrderRepository CustomerOrderRepository { get; private set; } =
            new CustomerOrderRepository(wmsDbContext);

        public ICustomerRepository CustomerRepository { get; private set; } =
            new CustomerRepository(wmsDbContext);

        public IIncomingOrderProductRepository IncomingOrderProductRepository
        {
            get;
            private set;
        } = new IncomingOrderProductRepository(wmsDbContext);

        public IIncomingOrderRepository IncomingOrderRepository { get; private set; } =
            new IncomingOrderRepository(wmsDbContext);

        public IInventoryRepository InventoryRepository { get; private set; } =
            new InventoryRepository(wmsDbContext);

        public IProductRepository ProductRepository { get; private set; } =
            new ProductRepository(wmsDbContext);

        public IProductRackRepository ProductRackRepository { get; private set; } =
            new ProductRackRepository(wmsDbContext);

        public IProductShopRepository ProductShopRepository { get; private set; } =
            new ProductShopRepository(wmsDbContext);

        public IProductSkuRepository ProductSkuRepository { get; private set; } =
            new ProductSkuRepository(wmsDbContext);

        public IRackRepository RackRepository { get; private set; } =
            new RackRepository(wmsDbContext);

        public IRefundOrderProductRepository RefundOrderProductRepository { get; private set; } =
            new RefundOrderProductRepository(wmsDbContext);

        public IRefundOrderRepository RefundOrderRepository { get; private set; } =
            new RefundOrderRepository(wmsDbContext);

        public IShopRepository ShopRepository { get; private set; } =
            new ShopRepository(wmsDbContext);

        public IStaffRepository StaffRepository { get; private set; } =
            new StaffRepository(wmsDbContext);
        public IStaffNotificationRepository StaffNotificationRepository { get; private set; } =
            new StaffNotificationRepository(wmsDbContext);

        public IVendorRepository VendorRepository { get; private set; } =
            new VendorRepository(wmsDbContext);

        public IWarehouseRepository WarehouseRepository { get; private set; } =
            new WarehouseRepository(wmsDbContext);

        public IZoneRepository ZoneRepository { get; private set; } =
            new ZoneRepository(wmsDbContext);

        public IZoneStaffRepository ZoneStaffRepository { get; private set; } =
            new ZoneStaffRepository(wmsDbContext);

        public async Task<int> CommitAsync()
        {
            return await wmsDbContext.SaveChangesAsync();
        }

        public async void Dispose()
        {
            await wmsDbContext.DisposeAsync();
        }
    }
}
