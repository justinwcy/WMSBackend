using Microsoft.EntityFrameworkCore;
using WMSBackend.Data;
using WMSBackend.Interfaces;
using WMSBackend.Models;

namespace WMSBackend.Repositories
{
    public class CustomerRepository : UserRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DbContext context)
            : base(context) { }

        public DbSet<Customer> CustomerContext => ((WmsDbContext)Context).Customers;

        public override async Task<Customer?> GetAsync(string id, bool isGetRelations)
        {
            var customers = await GetAllAsync(isGetRelations);
            return customers.FirstOrDefault(customer => customer.Id == id);
        }

        public override async Task<IEnumerable<Customer>> GetAllAsync(bool isGetRelations)
        {
            var customers = CustomerContext.AsQueryable();
            if (isGetRelations)
            {
                customers = customers.Include(customer => customer.CustomerOrders);
            }

            return await customers.ToListAsync();
        }

        public override async Task<IEnumerable<Customer>> FindAsync(
            Func<Customer, bool> predicate,
            bool isGetRelations
        )
        {
            var customers = await GetAllAsync(isGetRelations);
            return customers.Where(predicate);
        }

        public override async Task<bool> UpdateAsync(Customer customer)
        {
            var foundCustomer = await GetAsync(customer.Id, false);
            if (foundCustomer != null)
            {
                foundCustomer.FirstName = customer.FirstName;
                foundCustomer.LastName = customer.LastName;
                foundCustomer.Address = customer.Address;

                return true;
            }

            return false;
        }
    }
}
