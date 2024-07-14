namespace WMSBackend.Models
{
    public class IncomingOrderProduct
    {
        public Guid Id { get; set; }
        public int IncomingOrderId { get; set; }
        public IncomingOrder IncomingOrder { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public string Status { get; set; }

        public int Quantity { get; set; }
    }
}
