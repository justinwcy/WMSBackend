using System.Text.Json.Serialization;

namespace WMSBackend.Models
{
    public class Rack
    {
        public int Id { get; set; }

        // one to many relationship
        public int ZoneId { get; set; }

        [JsonIgnore]
        public Zone Zone { get; set; }

        // many to many relationship
        public List<Product> Products { get; set; }

        public string Name { get; set; }

        public double MaxWeight { get; set; }

        public double Height { get; set; }

        public double Width { get; set; }

        public double Depth { get; set; }
    }
}
