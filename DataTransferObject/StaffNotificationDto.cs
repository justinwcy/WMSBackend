namespace WMSBackend.DataTransferObject
{
    public class StaffNotificationDto
    {
        public string StaffId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsRead { get; set; }
    }
}
