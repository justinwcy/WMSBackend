using WMSBackend.Data;
using WMSBackend.Interfaces;

namespace WMSBackend.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WmsDbContext _wmsDbContext;

        public UnitOfWork(WmsDbContext wmsDbContext)
        {
            _wmsDbContext = wmsDbContext;
            CompanyRepository = new CompanyRepository(wmsDbContext);
            CourierRepository = new CourierRepository(wmsDbContext);
            CustomerOrderDetailRepository = new CustomerOrderDetailRepository(wmsDbContext);
            CustomerOrderRepository = new CustomerOrderRepository(wmsDbContext);
            CustomerRepository = new CustomerRepository(wmsDbContext);
            IncomingOrderRepository = new IncomingOrderRepository(wmsDbContext);
            InventoryRepository = new InventoryRepository(wmsDbContext);
            ProductRepository = new ProductRepository(wmsDbContext);
            RackRepository = new RackRepository(wmsDbContext);
            RefundOrderRepository = new RefundOrderRepository(wmsDbContext);
            ShopRepository = new ShopRepository(wmsDbContext);
            StaffRepository = new StaffRepository(wmsDbContext);
            WarehouseRepository = new WarehouseRepository(wmsDbContext);
            ZoneRepository = new ZoneRepository(wmsDbContext);
        }

        public ICompanyRepository CompanyRepository { get; private set; }

        public ICourierRepository CourierRepository { get; private set; }

        public ICustomerOrderDetailRepository CustomerOrderDetailRepository { get; private set; }

        public ICustomerOrderRepository CustomerOrderRepository { get; private set; }

        public ICustomerRepository CustomerRepository { get; private set; }

        public IIncomingOrderRepository IncomingOrderRepository { get; private set; }

        public IInventoryRepository InventoryRepository { get; private set; }

        public IProductRepository ProductRepository { get; private set; }

        public IRackRepository RackRepository { get; private set; }

        public IRefundOrderRepository RefundOrderRepository { get; private set; }

        public IShopRepository ShopRepository { get; private set; }

        public IStaffRepository StaffRepository { get; private set; }

        public IWarehouseRepository WarehouseRepository { get; private set; }

        public IZoneRepository ZoneRepository { get; private set; }

        public async Task<int> CommitAsync()
        {
            return await _wmsDbContext.SaveChangesAsync();
        }

        public async void Dispose()
        {
            await _wmsDbContext.DisposeAsync();
        }
    }
}
