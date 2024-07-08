using Microsoft.EntityFrameworkCore;
using WMSBackend.Data;
using WMSBackend.Interfaces;
using WMSBackend.Models;

namespace WMSBackend.Repositories
{
    public class CustomerOrderRepository
        : GenericRepository<CustomerOrder>,
            ICustomerOrderRepository
    {
        public CustomerOrderRepository(DbContext context)
            : base(context) { }

        public DbSet<CustomerOrder> CustomerOrderContext => ((WmsDbContext)Context).CustomerOrders;

        public override async Task<IEnumerable<CustomerOrder>> GetAllAsync(bool isGetRelations)
        {
            var customerOrders = CustomerOrderContext.AsQueryable();
            if (isGetRelations)
            {
                customerOrders = customerOrders
                    .Include(customerOrders => customerOrders.Customer)
                    .Include(customerOrders => customerOrders.CustomerOrderDetails)
                    .Include(customerOrders => customerOrders.Courier);
            }

            return await customerOrders.ToListAsync();
        }

        public async Task<List<CustomerOrder>> GetAllAsync(
            bool isGetCustomer,
            bool isGetCustomerOrderDetails,
            bool isGetCourier
        )
        {
            var customerOrders = CustomerOrderContext.AsQueryable();
            if (isGetCustomer)
            {
                customerOrders = customerOrders.Include(customerOrder => customerOrder.Customer);
            }

            if (isGetCustomerOrderDetails)
            {
                customerOrders = customerOrders.Include(customerOrder =>
                    customerOrder.CustomerOrderDetails
                );
            }

            if (isGetCourier)
            {
                customerOrders = customerOrders.Include(customerOrder => customerOrder.Courier);
            }

            return await customerOrders.ToListAsync();
        }

        public override async Task<CustomerOrder?> GetAsync(int id, bool isGetRelations)
        {
            var customerOrders = await GetAllAsync(isGetRelations);
            return customerOrders.FirstOrDefault(customerOrder => customerOrder.Id == id);
        }

        public async Task<CustomerOrder?> GetAsync(
            int id,
            bool isGetCustomer,
            bool isGetCustomerOrderDetail,
            bool isGetCourier
        )
        {
            var customerOrders = await GetAllAsync(
                isGetCustomer,
                isGetCustomerOrderDetail,
                isGetCourier
            );
            return customerOrders.FirstOrDefault(customerOrder => customerOrder.Id == id);
        }

        public override async Task<IEnumerable<CustomerOrder>> FindAsync(
            Func<CustomerOrder, bool> predicate,
            bool isGetRelations
        )
        {
            var customerOrders = await GetAllAsync(isGetRelations);
            return customerOrders.Where(predicate);
        }

        public override async Task<bool> UpdateAsync(CustomerOrder customerOrder)
        {
            var foundCustomerOrder = await GetAsync(customerOrder.Id, false);
            if (foundCustomerOrder != null)
            {
                foundCustomerOrder.ExpectedArrivalDate = customerOrder.ExpectedArrivalDate;
                foundCustomerOrder.OrderCreationDate = customerOrder.OrderCreationDate;
                foundCustomerOrder.OrderAddress = customerOrder.OrderAddress;
                return true;
            }

            return false;
        }
    }
}
