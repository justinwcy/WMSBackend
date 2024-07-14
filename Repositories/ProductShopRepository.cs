using Microsoft.EntityFrameworkCore;
using WMSBackend.Data;
using WMSBackend.Interfaces;
using WMSBackend.Models;

namespace WMSBackend.Repositories
{
    public class ProductShopRepository : GenericRepository<ProductShop>, IProductShopRepository
    {
        public ProductShopRepository(DbContext context)
            : base(context) { }

        public DbSet<ProductShop> ProductShopContext => ((WmsDbContext)Context).ProductShops;

        public override async Task<IEnumerable<ProductShop>> GetAllAsync(bool isGetRelations)
        {
            var productShops = ProductShopContext.AsQueryable();
            return await productShops.ToListAsync();
        }

        public override async Task<ProductShop?> GetAsync(Guid id, bool isGetRelations)
        {
            var productShops = await GetAllAsync(isGetRelations);
            return productShops.FirstOrDefault(product => product.Id == id);
        }

        public override async Task<IEnumerable<ProductShop>> FindAsync(
            Func<ProductShop, bool> predicate,
            bool isGetRelations
        )
        {
            var products = await GetAllAsync(isGetRelations);
            return products.Where(predicate);
        }

        public override async Task<bool> UpdateAsync(ProductShop productShop)
        {
            var foundProductShop = await GetAsync(productShop.Id, false);
            if (foundProductShop != null)
            {
                foundProductShop.ShopId = productShop.ShopId;
                foundProductShop.ProductId = productShop.ProductId;
                return true;
            }

            return false;
        }
    }
}
