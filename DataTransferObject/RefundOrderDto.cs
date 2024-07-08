using WMSBackend.Models;

namespace WMSBackend.DataTransferObject
{
    public class RefundOrderDto
    {
        public string RefundReason { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public static RefundOrderDto ToDto(RefundOrder refundOrder)
        {
            var outputDto = new RefundOrderDto
            {
                RefundReason = refundOrder.RefundReason,
                Status = refundOrder.Status,
            };
            return outputDto;
        }
    }
}
