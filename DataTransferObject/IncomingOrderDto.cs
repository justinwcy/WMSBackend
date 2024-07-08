using WMSBackend.Models;

namespace WMSBackend.DataTransferObject
{
    public class IncomingOrderDto
    {
        public DateTime IncomingDate { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
