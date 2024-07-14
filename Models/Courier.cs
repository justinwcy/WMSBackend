using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WMSBackend.Models
{
    public class Courier
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public required decimal Price { get; set; }

        public string Remark { get; set; }

        // one to many relationship
        [JsonIgnore]
        public List<CustomerOrder> CustomerOrders { get; set; }
    }
}
