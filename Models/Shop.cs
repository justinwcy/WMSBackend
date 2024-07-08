namespace WMSBackend.Models
{
    public class Shop
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Platform { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Website { get; set; } = string.Empty;

        // many to many relationship
        public List<Product> Products { get; set; }
    }
}
