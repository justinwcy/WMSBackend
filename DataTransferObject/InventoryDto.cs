namespace WMSBackend.DataTransferObject
{
    public class InventoryDto
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public int DaysLeadTime { get; set; }
    }
}
