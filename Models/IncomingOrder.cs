namespace WMSBackend.Models
{
    public class IncomingOrder
    {
        public int Id { get; set; }

        public DateTime IncomingDate { get; set; }

        public string Status { get; set; } = string.Empty;

        // many to many relationship
        public List<Product> Products { get; set; }
    }
}
