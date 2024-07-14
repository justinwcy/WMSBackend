namespace WMSBackend.Models
{
    public class IncomingOrder
    {
        public int Id { get; set; }
        public string PONumber { get; set; }

        public DateTime IncomingDate { get; set; }

        public DateTime ReceivingDate { get; set; }

        public string Status { get; set; } = string.Empty;

        // many to many relationship
        public List<Product> Products { get; set; }

        // one to many relationship
        public string VendorId { get; set; }
        public Vendor Vendor { get; set; }
    }
}
