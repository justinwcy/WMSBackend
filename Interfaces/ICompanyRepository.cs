using WMSBackend.Models;

namespace WMSBackend.Interfaces
{
    public interface ICompanyRepository : IGenericRepository<Company>
    {
        Task<List<Company>> GetAllAsync(bool isGetStaff, bool isGetWarehouse);
        Task<Company?> GetAsync(int id, bool isGetStaff, bool isGetWarehouse);
    }
}
