using System.Text.Json.Serialization;

namespace WMSBackend.Models
{
    public class Warehouse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Address { get; set; }

        // one to many relationship
        [JsonIgnore]
        public Company Company { get; set; }
        public int CompanyId { get; set; }

        // one to many relationship
        public List<Zone> Zones { get; set; }
    }
}
