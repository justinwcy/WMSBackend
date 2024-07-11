namespace WMSBackend.DataTransferObject
{
    public class CustomerOrderDto
    {
        public DateTime ExpectedArrivalDate { get; set; }

        public DateTime OrderCreationDate { get; set; }

        public string OrderAddress { get; set; }
    }
}
