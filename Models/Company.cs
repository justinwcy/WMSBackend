using System.ComponentModel.DataAnnotations.Schema;

namespace WMSBackend.Models
{
    public class Company
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public List<Warehouse> Warehouses { get; set; }

        public List<Staff> Staffs { get; set; }
    }
}
