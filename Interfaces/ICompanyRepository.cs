using WMSBackend.Models;

namespace WMSBackend.Interfaces
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<List<Company>> GetAllAsync(bool isGetStaff, bool isGetWarehouse);
        Task<Company?> GetAsync(Guid id, bool isGetStaff, bool isGetWarehouse);
    }
}
