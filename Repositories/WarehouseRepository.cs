using Microsoft.EntityFrameworkCore;
using WMSBackend.Data;
using WMSBackend.Interfaces;
using WMSBackend.Models;

namespace WMSBackend.Repositories
{
    public class WarehouseRepository : GenericRepository<Warehouse>, IWarehouseRepository
    {
        public WarehouseRepository(DbContext context)
            : base(context) { }

        public DbSet<Warehouse> WarehouseContext => ((WmsDbContext)Context).Warehouses;

        public override async Task<IEnumerable<Warehouse>> GetAllAsync(bool isGetRelations)
        {
            var warehouses = WarehouseContext.AsQueryable();
            if (isGetRelations)
            {
                warehouses = warehouses
                    .Include(warehouse => warehouse.Company)
                    .Include(warehouse => warehouse.Zones);
            }

            return await warehouses.ToListAsync();
        }

        public async Task<List<Warehouse>> GetAllAsync(bool isGetCompany, bool isGetZones)
        {
            var warehouses = WarehouseContext.AsQueryable();
            if (isGetCompany)
            {
                warehouses = warehouses.Include(warehouse => warehouse.Company);
            }

            if (isGetZones)
            {
                warehouses = warehouses.Include(warehouse => warehouse.Zones);
            }

            return await warehouses.ToListAsync();
        }

        public override async Task<Warehouse?> GetAsync(Guid id, bool isGetRelations)
        {
            var warehouses = await GetAllAsync(isGetRelations);
            return warehouses.FirstOrDefault(warehouse => warehouse.Id == id);
        }

        public async Task<Warehouse?> GetAsync(Guid id, bool isGetCompany, bool isGetZones)
        {
            var warehouses = await GetAllAsync(isGetCompany, isGetZones);
            return warehouses.FirstOrDefault(warehouse => warehouse.Id == id);
        }

        public override async Task<IEnumerable<Warehouse>> FindAsync(
            Func<Warehouse, bool> predicate,
            bool isGetRelations
        )
        {
            var warehouses = await GetAllAsync(isGetRelations);
            return warehouses.Where(predicate);
        }

        public override async Task<bool> UpdateAsync(Warehouse warehouse)
        {
            var foundWarehouse = await GetAsync(warehouse.Id, false);
            if (foundWarehouse != null)
            {
                foundWarehouse.Name = warehouse.Name;
                foundWarehouse.Address = warehouse.Address;

                return true;
            }

            return false;
        }
    }
}
