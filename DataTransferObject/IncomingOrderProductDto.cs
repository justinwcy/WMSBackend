namespace WMSBackend.DataTransferObject
{
    public class IncomingOrderProductDto
    {
        public int IncomingOrderId { get; set; }

        public int ProductId { get; set; }
        public string Status { get; set; }

        public int Quantity { get; set; }
    }
}
