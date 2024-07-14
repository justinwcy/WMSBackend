using Microsoft.EntityFrameworkCore;
using WMSBackend.Data;
using WMSBackend.Interfaces;
using WMSBackend.Models;

namespace WMSBackend.Repositories
{
    public class CustomerOrderDetailRepository
        : GenericRepository<CustomerOrderDetail>,
            ICustomerOrderDetailRepository
    {
        public CustomerOrderDetailRepository(DbContext context)
            : base(context) { }

        public DbSet<CustomerOrderDetail> CustomerOrderDetailContext =>
            ((WmsDbContext)Context).CustomerOrderDetails;

        public override async Task<IEnumerable<CustomerOrderDetail>> GetAllAsync(
            bool isGetRelations
        )
        {
            var customerOrderDetails = CustomerOrderDetailContext.AsQueryable();
            if (isGetRelations)
            {
                customerOrderDetails = customerOrderDetails
                    .Include(customerOrderDetail => customerOrderDetail.CustomerOrder)
                    .Include(customerOrderDetail => customerOrderDetail.Product);
            }

            return await customerOrderDetails.ToListAsync();
        }

        public async Task<List<CustomerOrderDetail>> GetAllAsync(
            bool isGetCustomerOrder,
            bool isGetProduct
        )
        {
            var customerOrderDetails = CustomerOrderDetailContext.AsQueryable();
            if (isGetCustomerOrder)
            {
                customerOrderDetails = customerOrderDetails.Include(customerOrderDetail =>
                    customerOrderDetail.CustomerOrder
                );
            }

            if (isGetProduct)
            {
                customerOrderDetails = customerOrderDetails.Include(customerOrderDetail =>
                    customerOrderDetail.Product
                );
            }

            return await customerOrderDetails.ToListAsync();
        }

        public override async Task<CustomerOrderDetail?> GetAsync(Guid id, bool isGetRelations)
        {
            var customerOrderDetails = await GetAllAsync(isGetRelations);
            return customerOrderDetails.FirstOrDefault(customerOrderDetail =>
                customerOrderDetail.Id == id
            );
        }

        public async Task<CustomerOrderDetail?> GetAsync(
            Guid id,
            bool isGetStaff,
            bool isGetWarehouse
        )
        {
            var customerOrderDetails = await GetAllAsync(isGetStaff, isGetWarehouse);
            return customerOrderDetails.FirstOrDefault(customerOrderDetail =>
                customerOrderDetail.Id == id
            );
        }

        public override async Task<IEnumerable<CustomerOrderDetail>> FindAsync(
            Func<CustomerOrderDetail, bool> predicate,
            bool isGetRelations
        )
        {
            var customerOrderDetails = await GetAllAsync(isGetRelations);
            return customerOrderDetails.Where(predicate);
        }

        public override async Task<bool> UpdateAsync(CustomerOrderDetail customerOrderDetail)
        {
            var foundCustomerOrderDetail = await GetAsync(customerOrderDetail.Id, false);
            if (foundCustomerOrderDetail != null)
            {
                foundCustomerOrderDetail.Quantity = customerOrderDetail.Quantity;
                foundCustomerOrderDetail.Status = customerOrderDetail.Status;
                return true;
            }

            return false;
        }
    }
}
