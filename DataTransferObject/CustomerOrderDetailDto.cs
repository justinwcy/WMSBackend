using WMSBackend.Models;

namespace WMSBackend.DataTransferObject
{
    public class CustomerOrderDetailDto
    {
        public int Quantity { get; set; }

        public string Status { get; set; }

        public string? OrderBin { get; set; }
    }
}
