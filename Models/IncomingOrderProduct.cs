namespace WMSBackend.Models
{
    public class IncomingOrderProduct
    {
        public int Id { get; set; }
        public int IncomingOrderId { get; set; }

        public int ProductId { get; set; }

        public string Status { get; set; }

        public int Quantity { get; set; }
    }
}
