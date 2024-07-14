using Microsoft.EntityFrameworkCore;
using WMSBackend.Data;
using WMSBackend.Interfaces;
using WMSBackend.Models;

namespace WMSBackend.Repositories
{
    public class ProductSkuRepository : GenericRepository<ProductSku>, IProductSkuRepository
    {
        public ProductSkuRepository(DbContext context)
            : base(context) { }

        public DbSet<ProductSku> ProductSkuContext => ((WmsDbContext)Context).ProductSkus;

        public override async Task<IEnumerable<ProductSku>> GetAllAsync(bool isGetRelations)
        {
            var productSkus = ProductSkuContext.AsQueryable();
            if (isGetRelations)
            {
                productSkus = productSkus.Include(product => product.Product);
            }

            return await productSkus.ToListAsync();
        }

        public override async Task<ProductSku?> GetAsync(Guid id, bool isGetRelations)
        {
            var productSkus = await GetAllAsync(isGetRelations);
            return productSkus.FirstOrDefault(product => product.Id == id);
        }

        public override async Task<IEnumerable<ProductSku>> FindAsync(
            Func<ProductSku, bool> predicate,
            bool isGetRelations
        )
        {
            var products = await GetAllAsync(isGetRelations);
            return products.Where(predicate);
        }

        public override async Task<bool> UpdateAsync(ProductSku productSku)
        {
            var foundProductSku = await GetAsync(productSku.Id, false);
            if (foundProductSku != null)
            {
                foundProductSku.Sku = productSku.Sku;
                foundProductSku.ProductId = productSku.ProductId;
                return true;
            }

            return false;
        }
    }
}
