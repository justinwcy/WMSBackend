using Microsoft.EntityFrameworkCore;
using WMSBackend.Data;
using WMSBackend.Interfaces;
using WMSBackend.Models;

namespace WMSBackend.Repositories
{
    public class RefundOrderProductRepository
        : GenericRepository<RefundOrderProduct>,
            IRefundOrderProductRepository
    {
        public RefundOrderProductRepository(DbContext context)
            : base(context) { }

        public DbSet<RefundOrderProduct> RefundOrderProductContext =>
            ((WmsDbContext)Context).RefundOrderProducts;

        public override async Task<IEnumerable<RefundOrderProduct>> GetAllAsync(bool isGetRelations)
        {
            var refundOrderProducts = RefundOrderProductContext.AsQueryable();
            return await refundOrderProducts.ToListAsync();
        }

        public override async Task<RefundOrderProduct?> GetAsync(int id, bool isGetRelations)
        {
            var refundOrderProducts = await GetAllAsync(isGetRelations);
            return refundOrderProducts.FirstOrDefault(refundOrderProduct =>
                refundOrderProduct.Id == id
            );
        }

        public override async Task<IEnumerable<RefundOrderProduct>> FindAsync(
            Func<RefundOrderProduct, bool> predicate,
            bool isGetRelations
        )
        {
            var refundOrderProducts = await GetAllAsync(isGetRelations);
            return refundOrderProducts.Where(predicate);
        }

        public override async Task<bool> UpdateAsync(RefundOrderProduct refundOrderProduct)
        {
            var foundRefundOrderProduct = await GetAsync(refundOrderProduct.Id, false);
            if (foundRefundOrderProduct != null)
            {
                foundRefundOrderProduct.Quantity = refundOrderProduct.Quantity;
                foundRefundOrderProduct.Status = refundOrderProduct.Status;

                return true;
            }

            return false;
        }
    }
}
