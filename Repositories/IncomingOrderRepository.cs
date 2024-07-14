using Microsoft.EntityFrameworkCore;
using WMSBackend.Data;
using WMSBackend.Interfaces;
using WMSBackend.Models;

namespace WMSBackend.Repositories
{
    public class IncomingOrderRepository
        : GenericRepository<IncomingOrder>,
            IIncomingOrderRepository
    {
        public IncomingOrderRepository(DbContext context)
            : base(context) { }

        public DbSet<IncomingOrder> IncomingOrderContext => ((WmsDbContext)Context).IncomingOrders;

        public override async Task<IEnumerable<IncomingOrder>> GetAllAsync(bool isGetRelations)
        {
            var incomingOrders = IncomingOrderContext.AsQueryable();
            if (isGetRelations)
            {
                incomingOrders = incomingOrders
                    .Include(incomingOrder => incomingOrder.Products)
                    .Include(incomingOrder => incomingOrder.Vendor);
            }

            return await incomingOrders.ToListAsync();
        }

        public async Task<List<IncomingOrder>> GetAllAsync(bool isGetProducts, bool isGetVendor)
        {
            var incomingOrders = IncomingOrderContext.AsQueryable();

            if (isGetProducts)
            {
                incomingOrders = incomingOrders.Include(incomingOrder => incomingOrder.Products);
            }

            if (isGetVendor)
            {
                incomingOrders = incomingOrders.Include(incomingOrder => incomingOrder.Vendor);
            }

            return await incomingOrders.ToListAsync();
        }

        public override async Task<IncomingOrder?> GetAsync(Guid id, bool isGetRelations)
        {
            var incomingOrders = await GetAllAsync(isGetRelations);
            return incomingOrders.FirstOrDefault(incomingOrder => incomingOrder.Id == id);
        }

        public async Task<IncomingOrder?> GetAsync(Guid id, bool isGetProducts, bool isGetVendor)
        {
            var incomingOrders = await GetAllAsync(isGetProducts, isGetVendor);

            return incomingOrders.FirstOrDefault(incomingOrder => incomingOrder.Id == id);
        }

        public override async Task<IEnumerable<IncomingOrder>> FindAsync(
            Func<IncomingOrder, bool> predicate,
            bool isGetRelations
        )
        {
            var incomingOrders = await GetAllAsync(isGetRelations);
            return incomingOrders.Where(predicate);
        }

        public override async Task<bool> UpdateAsync(IncomingOrder incomingOrder)
        {
            var foundIncomingOrder = await GetAsync(incomingOrder.Id, false);
            if (foundIncomingOrder != null)
            {
                foundIncomingOrder.IncomingDate = incomingOrder.IncomingDate;
                foundIncomingOrder.Status = incomingOrder.Status;

                return true;
            }

            return false;
        }
    }
}
