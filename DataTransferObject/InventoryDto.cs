using WMSBackend.Models;

namespace WMSBackend.DataTransferObject
{
    public class InventoryDto
    {
        public int ProductId { get; set; }

        public required int Quantity { get; set; }

        public int DaysLeadTime { get; set; }

        public static InventoryDto ToDto(Inventory inventory)
        {
            var outputDto = new InventoryDto
            {
                ProductId = inventory.ProductId,
                Quantity = inventory.Quantity,
                DaysLeadTime = inventory.DaysLeadTime
            };
            return outputDto;
        }
    }
}
