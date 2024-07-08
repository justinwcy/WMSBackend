using WMSBackend.Models;

namespace WMSBackend.DataTransferObject
{
    public class StaffDto
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string UserName { get; set; }

        public string Email { get; set; }
    }
}
