using System.Text.Json.Serialization;

namespace WMSBackend.Models
{
    public class CustomerOrderDetail
    {
        public Guid Id { get; set; }

        public int CustomerOrderId { get; set; }

        public int Quantity { get; set; }
        public string Status { get; set; }

        [JsonIgnore]
        public CustomerOrder CustomerOrder { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
