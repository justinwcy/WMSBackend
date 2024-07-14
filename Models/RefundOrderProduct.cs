namespace WMSBackend.Models
{
    public class RefundOrderProduct
    {
        public Guid Id { get; set; }

        public int RefundOrderId { get; set; }

        public int ProductId { get; set; }

        public string Status { get; set; }

        public int Quantity { get; set; }
    }
}
