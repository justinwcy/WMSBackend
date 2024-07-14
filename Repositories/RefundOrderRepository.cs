using Microsoft.EntityFrameworkCore;
using WMSBackend.Data;
using WMSBackend.Interfaces;
using WMSBackend.Models;

namespace WMSBackend.Repositories
{
    public class RefundOrderRepository : GenericRepository<RefundOrder>, IRefundOrderRepository
    {
        public RefundOrderRepository(DbContext context)
            : base(context) { }

        public DbSet<RefundOrder> RefundOrderContext => ((WmsDbContext)Context).RefundOrders;

        public override async Task<IEnumerable<RefundOrder>> GetAllAsync(bool isGetRelations)
        {
            var refundOrders = RefundOrderContext.AsQueryable();
            if (isGetRelations)
            {
                refundOrders = refundOrders.Include(incomingOrder => incomingOrder.Products);
            }

            return await refundOrders.ToListAsync();
        }

        public override async Task<RefundOrder?> GetAsync(Guid id, bool isGetRelations)
        {
            var refundOrders = await GetAllAsync(isGetRelations);
            return refundOrders.FirstOrDefault(refundOrder => refundOrder.Id == id);
        }

        public override async Task<IEnumerable<RefundOrder>> FindAsync(
            Func<RefundOrder, bool> predicate,
            bool isGetRelations
        )
        {
            var refundOrders = await GetAllAsync(isGetRelations);
            return refundOrders.Where(predicate);
        }

        public override async Task<bool> UpdateAsync(RefundOrder refundOrder)
        {
            var foundRefundOrder = await GetAsync(refundOrder.Id, false);
            if (foundRefundOrder != null)
            {
                foundRefundOrder.RefundReason = refundOrder.RefundReason;
                foundRefundOrder.Status = refundOrder.Status;

                return true;
            }

            return false;
        }
    }
}
