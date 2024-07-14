using Microsoft.EntityFrameworkCore;
using WMSBackend.Data;
using WMSBackend.Interfaces;
using WMSBackend.Models;

namespace WMSBackend.Repositories
{
    public class IncomingOrderProductRepository
        : GenericRepository<IncomingOrderProduct>,
            IIncomingOrderProductRepository
    {
        public IncomingOrderProductRepository(DbContext context)
            : base(context) { }

        public DbSet<IncomingOrderProduct> IncomingOrderProductContext =>
            ((WmsDbContext)Context).IncomingOrderProducts;

        public override async Task<IEnumerable<IncomingOrderProduct>> GetAllAsync(
            bool isGetRelations
        )
        {
            var incomingOrderProducts = IncomingOrderProductContext.AsQueryable();
            return await incomingOrderProducts.ToListAsync();
        }

        public override async Task<IncomingOrderProduct?> GetAsync(int id, bool isGetRelations)
        {
            var incomingOrderProducts = await GetAllAsync(isGetRelations);
            return incomingOrderProducts.FirstOrDefault(incomingOrderProduct =>
                incomingOrderProduct.Id == id
            );
        }

        public override async Task<IEnumerable<IncomingOrderProduct>> FindAsync(
            Func<IncomingOrderProduct, bool> predicate,
            bool isGetRelations
        )
        {
            var incomingOrderProducts = await GetAllAsync(isGetRelations);
            return incomingOrderProducts.Where(predicate);
        }

        public override async Task<bool> UpdateAsync(IncomingOrderProduct incomingOrderProduct)
        {
            var foundIncomingOrderProduct = await GetAsync(incomingOrderProduct.Id, false);
            if (foundIncomingOrderProduct != null)
            {
                foundIncomingOrderProduct.Quantity = incomingOrderProduct.Quantity;
                foundIncomingOrderProduct.Status = incomingOrderProduct.Status;

                return true;
            }

            return false;
        }
    }
}
