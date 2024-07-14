using System.Text.Json.Serialization;

namespace WMSBackend.Models
{
    public class Bin
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // one to many relationship
        [JsonIgnore]
        public List<CustomerOrder> CustomerOrders { get; set; }
    }
}
