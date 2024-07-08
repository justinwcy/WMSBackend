using System.Text.Json.Serialization;

namespace WMSBackend.Models
{
    public class CustomerOrder
    {
        public int Id { get; set; }

        public DateTime ExpectedArrivalDate { get; set; }

        public DateTime OrderCreationDate { get; set; }

        public string OrderAddress { get; set; }

        // one to many relationship
        public string CustomerId { get; set; }

        [JsonIgnore]
        public Customer Customer { get; set; }

        // one to many relationship
        public List<CustomerOrderDetail> CustomerOrderDetails { get; set; }

        // one to many relationship
        public Courier Courier { get; set; }
        public int CourierId { get; set; }
    }
}
