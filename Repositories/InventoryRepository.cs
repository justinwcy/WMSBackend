using Microsoft.EntityFrameworkCore;
using WMSBackend.Data;
using WMSBackend.Interfaces;
using WMSBackend.Models;

namespace WMSBackend.Repositories
{
    public class InventoryRepository : GenericRepository<Inventory>, IInventoryRepository
    {
        public InventoryRepository(DbContext context)
            : base(context) { }

        public DbSet<Inventory> InventoryContext => ((WmsDbContext)Context).Inventories;

        public override async Task<IEnumerable<Inventory>> GetAllAsync(bool isGetRelations)
        {
            var inventories = InventoryContext.AsQueryable();
            if (isGetRelations)
            {
                inventories = inventories.Include(inventory => inventory.Product);
            }

            return await inventories.ToListAsync();
        }

        public override async Task<Inventory?> GetAsync(Guid id, bool isGetRelations)
        {
            var inventories = await GetAllAsync(isGetRelations);
            return inventories.FirstOrDefault(inventory => inventory.ProductId == id);
        }

        public override async Task<IEnumerable<Inventory>> FindAsync(
            Func<Inventory, bool> predicate,
            bool isGetRelations
        )
        {
            var inventories = await GetAllAsync(isGetRelations);
            return inventories.Where(predicate);
        }

        public override async Task<bool> UpdateAsync(Inventory inventory)
        {
            var foundInventory = await GetAsync(inventory.ProductId, false);
            if (foundInventory != null)
            {
                foundInventory.Quantity = foundInventory.Quantity;
                foundInventory.DaysLeadTime = foundInventory.DaysLeadTime;

                return true;
            }

            return false;
        }
    }
}
