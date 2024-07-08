using System.ComponentModel.DataAnnotations;

namespace WMSBackend.Models
{
    public class Inventory
    {
        [Key]
        public int ProductId { get; set; }

        // one to one relationship
        public Product Product { get; set; }

        public required int Quantity { get; set; }

        public int DaysLeadTime { get; set; }
    }
}
