namespace WMSBackend.Models
{
    public class StaffNotification
    {
        public Guid Id { get; set; }

        // one to many relationship
        public Staff Staff { get; set; }
        public string StaffId { get; set; }
        public DateTime NotificationDate { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsRead { get; set; }
    }
}
