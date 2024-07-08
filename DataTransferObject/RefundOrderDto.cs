using WMSBackend.Models;

namespace WMSBackend.DataTransferObject
{
    public class RefundOrderDto
    {
        public string RefundReason { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
    }
}
