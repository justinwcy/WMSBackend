using Microsoft.AspNetCore.Identity;

namespace WMSBackend.Models
{
    public class Vendor : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; } = string.Empty;

        [PersonalData]
        public string LastName { get; set; } = string.Empty;

        [PersonalData]
        public string Address { get; set; }

        [PersonalData]
        public string PhoneNumber { get; set; }

        public List<IncomingOrder> IncomingOrders { get; set; }
    }
}
