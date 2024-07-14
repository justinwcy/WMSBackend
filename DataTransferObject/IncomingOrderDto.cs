namespace WMSBackend.DataTransferObject
{
    public class IncomingOrderDto
    {
        public string PONumber { get; set; }
        public DateTime IncomingDate { get; set; }
        public DateTime ReceivingDate { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
