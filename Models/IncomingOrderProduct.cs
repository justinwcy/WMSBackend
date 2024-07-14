namespace WMSBackend.Models
{
    public class IncomingOrderProduct
    {
        public Guid Id { get; set; }
        public Guid IncomingOrderId { get; set; }
        public IncomingOrder IncomingOrder { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public string Status { get; set; }

        public int Quantity { get; set; }
    }
}
