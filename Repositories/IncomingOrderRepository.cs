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
                incomingOrders = incomingOrders.Include(incomingOrder => incomingOrder.Products);
            }

            return await incomingOrders.ToListAsync();
        }

        public override async Task<IncomingOrder?> GetAsync(int id, bool isGetRelations)
        {
            var incomingOrders = await GetAllAsync(isGetRelations);
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
