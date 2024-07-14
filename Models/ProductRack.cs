namespace WMSBackend.Models
{
    public class ProductRack
    {
        public Guid Id { get; set; }
        public int ProductId { get; set; }

        public int RackId { get; set; }
    }
}
