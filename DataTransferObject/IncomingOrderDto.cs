using WMSBackend.Models;

namespace WMSBackend.DataTransferObject
{
    public class IncomingOrderDto
    {
        public DateTime IncomingDate { get; set; }

        public string Status { get; set; } = string.Empty;

        public static IncomingOrderDto ToDto(IncomingOrder incomingOrder)
        {
            var outputDto = new IncomingOrderDto
            {
                IncomingDate = incomingOrder.IncomingDate,
                Status = incomingOrder.Status,
            };
            return outputDto;
        }
    }
}
