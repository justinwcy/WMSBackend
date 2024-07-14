using Microsoft.EntityFrameworkCore;
using WMSBackend.Data;
using WMSBackend.Interfaces;
using WMSBackend.Models;

namespace WMSBackend.Repositories
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(DbContext context)
            : base(context) { }

        public DbSet<Company> CompanyContext => ((WmsDbContext)Context).Companies;

        public override async Task<IEnumerable<Company>> GetAllAsync(bool isGetRelations)
        {
            var companies = CompanyContext.AsQueryable();
            if (isGetRelations)
            {
                companies = companies
                    .Include(company => company.Staffs)
                    .Include(company => company.Warehouses);
            }

            return await companies.ToListAsync();
        }

        public async Task<List<Company>> GetAllAsync(bool isGetStaffs, bool isGetWarehouses)
        {
            var companies = CompanyContext.AsQueryable();
            if (isGetStaffs)
            {
                companies = companies.Include(company => company.Staffs);
            }

            if (isGetWarehouses)
            {
                companies = companies.Include(company => company.Warehouses);
            }

            return await companies.ToListAsync();
        }

        public override async Task<Company?> GetAsync(Guid id, bool isGetRelations)
        {
            var companies = await GetAllAsync(isGetRelations);
            return companies.FirstOrDefault(company => company.Id == id);
        }

        public async Task<Company?> GetAsync(Guid id, bool isGetStaff, bool isGetWarehouse)
        {
            var companies = await GetAllAsync(isGetStaff, isGetWarehouse);
            return companies.FirstOrDefault(company => company.Id == id);
        }

        public override async Task<IEnumerable<Company>> FindAsync(
            Func<Company, bool> predicate,
            bool isGetRelations
        )
        {
            var companies = await GetAllAsync(isGetRelations);
            return companies.Where(predicate);
        }

        public override async Task<bool> UpdateAsync(Company company)
        {
            var foundCompany = await GetAsync(company.Id, false);
            if (foundCompany != null)
            {
                foundCompany.Name = company.Name;
                return true;
            }
            return false;
        }
    }
}
