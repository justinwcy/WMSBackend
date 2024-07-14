using Microsoft.EntityFrameworkCore;
using WMSBackend.Data;
using WMSBackend.Interfaces;
using WMSBackend.Models;

namespace WMSBackend.Repositories
{
    public class ShopRepository : GenericRepository<Shop>, IShopRepository
    {
        public ShopRepository(DbContext context)
            : base(context) { }

        public DbSet<Shop> ShopContext => ((WmsDbContext)Context).Shops;

        public override async Task<IEnumerable<Shop>> GetAllAsync(bool isGetRelations)
        {
            var shops = ShopContext.AsQueryable();
            if (isGetRelations)
            {
                shops = shops.Include(shop => shop.Products);
            }

            return await shops.ToListAsync();
        }

        public override async Task<Shop?> GetAsync(Guid id, bool isGetRelations)
        {
            var shops = await GetAllAsync(isGetRelations);
            return shops.FirstOrDefault(shop => shop.Id == id);
        }

        public override async Task<IEnumerable<Shop>> FindAsync(
            Func<Shop, bool> predicate,
            bool isGetRelations
        )
        {
            var shops = await GetAllAsync(isGetRelations);
            return shops.Where(predicate);
        }

        public override async Task<bool> UpdateAsync(Shop shop)
        {
            var foundShop = await GetAsync(shop.Id, false);
            if (foundShop != null)
            {
                foundShop.Name = shop.Name;
                foundShop.Platform = shop.Platform;
                foundShop.Address = shop.Address;
                foundShop.Website = shop.Website;

                return true;
            }

            return false;
        }
    }
}
