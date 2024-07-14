using Microsoft.EntityFrameworkCore;
using WMSBackend.Data;
using WMSBackend.Interfaces;
using WMSBackend.Models;

namespace WMSBackend.Repositories
{
    public class CourierRepository : GenericRepository<Courier>, ICourierRepository
    {
        public CourierRepository(DbContext context)
            : base(context) { }

        public DbSet<Courier> CourierContext => ((WmsDbContext)Context).Couriers;

        public override async Task<IEnumerable<Courier>> GetAllAsync(bool isGetRelations)
        {
            var couriers = CourierContext.AsQueryable();
            if (isGetRelations)
            {
                couriers = couriers.Include(courier => courier.CustomerOrders);
            }

            return await couriers.ToListAsync();
        }

        public override async Task<Courier?> GetAsync(Guid id, bool isGetRelations)
        {
            var couriers = await GetAllAsync(isGetRelations);
            return couriers.FirstOrDefault(courier => courier.Id == id);
        }

        public override async Task<IEnumerable<Courier>> FindAsync(
            Func<Courier, bool> predicate,
            bool isGetRelations
        )
        {
            var couriers = await GetAllAsync(isGetRelations);
            return couriers.Where(predicate);
        }

        public override async Task<bool> UpdateAsync(Courier courier)
        {
            var foundCourier = await GetAsync(courier.Id, false);
            if (foundCourier != null)
            {
                foundCourier.Name = courier.Name;
                foundCourier.Price = courier.Price;
                foundCourier.Remark = courier.Remark;

                return true;
            }

            return false;
        }
    }
}
