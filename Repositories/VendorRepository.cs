using Microsoft.EntityFrameworkCore;
using WMSBackend.Data;
using WMSBackend.Interfaces;
using WMSBackend.Models;

namespace WMSBackend.Repositories
{
    public class VendorRepository : UserRepository<Vendor>, IVendorRepository
    {
        public VendorRepository(DbContext context)
            : base(context) { }

        public DbSet<Vendor> VendorContext => ((WmsDbContext)Context).Vendors;

        public override async Task<Vendor?> GetAsync(string id, bool isGetRelations)
        {
            var vendors = await GetAllAsync(isGetRelations);
            return vendors.FirstOrDefault(vendor => vendor.Id == id);
        }

        public override async Task<IEnumerable<Vendor>> GetAllAsync(bool isGetRelations)
        {
            var vendors = VendorContext.AsQueryable();
            if (isGetRelations)
            {
                vendors = vendors.Include(vendor => vendor.IncomingOrders);
            }

            return await vendors.ToListAsync();
        }

        public override async Task<IEnumerable<Vendor>> FindAsync(
            Func<Vendor, bool> predicate,
            bool isGetRelations
        )
        {
            var vendors = await GetAllAsync(isGetRelations);
            return vendors.Where(predicate);
        }

        public override async Task<bool> UpdateAsync(Vendor vendor)
        {
            var foundVendor = await GetAsync(vendor.Id, false);
            if (foundVendor != null)
            {
                foundVendor.FirstName = vendor.FirstName;
                foundVendor.LastName = vendor.LastName;
                foundVendor.Address = vendor.Address;
                foundVendor.PhoneNumber = vendor.PhoneNumber;

                return true;
            }

            return false;
        }
    }
}
