namespace WMSBackend.DataTransferObject
{
    public class RefundOrderProductDto
    {
        public Guid RefundOrderId { get; set; }

        public Guid ProductId { get; set; }

        public string Status { get; set; }

        public int Quantity { get; set; }
    }
}
