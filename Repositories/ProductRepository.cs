using Microsoft.EntityFrameworkCore;
using WMSBackend.Data;
using WMSBackend.Interfaces;
using WMSBackend.Models;

namespace WMSBackend.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DbContext context)
            : base(context) { }

        public DbSet<Product> ProductContext => ((WmsDbContext)Context).Products;

        public override async Task<IEnumerable<Product>> GetAllAsync(bool isGetRelations)
        {
            var products = ProductContext.AsQueryable();
            if (isGetRelations)
            {
                products = products
                    .Include(product => product.IncomingOrders)
                    .Include(product => product.RefundOrders)
                    .Include(product => product.Shops)
                    .Include(product => product.Racks)
                    .Include(product => product.CurrentInventory)
                    .Include(product => product.CustomerOrderDetails);
            }

            return await products.ToListAsync();
        }

        public async Task<List<Product>> GetAllAsync(
            bool isGetIncomingOrders,
            bool isGetRefundOrders,
            bool isGetShops,
            bool isGetRacks,
            bool isGetCurrentInventory,
            bool isGetCustomerOrderDetails
        )
        {
            var products = ProductContext.AsQueryable();

            if (isGetIncomingOrders)
            {
                products = products.Include(product => product.IncomingOrders);
            }

            if (isGetRefundOrders)
            {
                products = products.Include(product => product.RefundOrders);
            }

            if (isGetShops)
            {
                products = products.Include(product => product.Shops);
            }

            if (isGetRacks)
            {
                products = products.Include(product => product.Racks);
            }

            if (isGetCurrentInventory)
            {
                products = products.Include(product => product.CurrentInventory);
            }

            if (isGetCustomerOrderDetails)
            {
                products = products.Include(product => product.CustomerOrderDetails);
            }

            return await products.ToListAsync();
        }

        public override async Task<Product?> GetAsync(Guid id, bool isGetRelations)
        {
            var products = await GetAllAsync(isGetRelations);
            return products.FirstOrDefault(product => product.Id == id);
        }

        public async Task<Product?> GetAsync(
            Guid id,
            bool isGetIncomingOrders,
            bool isGetRefundOrders,
            bool isGetShops,
            bool isGetRacks,
            bool isGetCurrentInventory,
            bool isGetCustomerOrderDetails
        )
        {
            var products = await GetAllAsync(
                isGetIncomingOrders,
                isGetRefundOrders,
                isGetShops,
                isGetRacks,
                isGetCurrentInventory,
                isGetCustomerOrderDetails
            );

            return products.FirstOrDefault(product => product.Id == id);
        }

        public override async Task<IEnumerable<Product>> FindAsync(
            Func<Product, bool> predicate,
            bool isGetRelations
        )
        {
            var products = await GetAllAsync(isGetRelations);
            return products.Where(predicate);
        }

        public override async Task<bool> UpdateAsync(Product product)
        {
            var foundProduct = await GetAsync(product.Id, false);
            if (foundProduct != null)
            {
                foundProduct.Name = product.Name;
                foundProduct.Description = product.Description;
                foundProduct.Price = product.Price;
                foundProduct.Tag = product.Tag;
                foundProduct.Weight = product.Weight;
                foundProduct.Height = product.Height;
                foundProduct.Length = product.Length;
                foundProduct.Width = product.Width;

                return true;
            }

            return false;
        }
    }
}
