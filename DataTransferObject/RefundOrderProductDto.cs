namespace WMSBackend.DataTransferObject
{
    public class RefundOrderProductDto
    {
        public int RefundOrderId { get; set; }

        public int ProductId { get; set; }

        public string Status { get; set; }

        public int Quantity { get; set; }
    }
}
