using System.ComponentModel.DataAnnotations;

namespace WMSBackend.Models
{
    public class Inventory
    {
        public Guid Id { get; set; }

        public int ProductId { get; set; }

        // one to one relationship
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public int DaysLeadTime { get; set; }
    }
}
