using Microsoft.EntityFrameworkCore;
using WMSBackend.Data;
using WMSBackend.Interfaces;
using WMSBackend.Models;

namespace WMSBackend.Repositories
{
    public class ProductRackRepository : GenericRepository<ProductRack>, IProductRackRepository
    {
        public ProductRackRepository(DbContext context)
            : base(context) { }

        public DbSet<ProductRack> ProductRackContext => ((WmsDbContext)Context).ProductRacks;

        public override async Task<IEnumerable<ProductRack>> GetAllAsync(bool isGetRelations)
        {
            var productRacks = ProductRackContext.AsQueryable();
            return await productRacks.ToListAsync();
        }

        public override async Task<ProductRack?> GetAsync(Guid id, bool isGetRelations)
        {
            var productRacks = await GetAllAsync(isGetRelations);
            return productRacks.FirstOrDefault(product => product.Id == id);
        }

        public override async Task<IEnumerable<ProductRack>> FindAsync(
            Func<ProductRack, bool> predicate,
            bool isGetRelations
        )
        {
            var products = await GetAllAsync(isGetRelations);
            return products.Where(predicate);
        }

        public override async Task<bool> UpdateAsync(ProductRack productRack)
        {
            var foundProductRack = await GetAsync(productRack.Id, false);
            if (foundProductRack != null)
            {
                foundProductRack.RackId = productRack.RackId;
                foundProductRack.ProductId = productRack.ProductId;
                return true;
            }

            return false;
        }
    }
}
