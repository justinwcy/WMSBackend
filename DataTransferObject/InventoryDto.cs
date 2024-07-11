namespace WMSBackend.DataTransferObject
{
    public class InventoryDto
    {
        public int ProductId { get; set; }

        public required int Quantity { get; set; }

        public int DaysLeadTime { get; set; }
    }
}
