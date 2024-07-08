using Microsoft.AspNetCore.Identity;

namespace WMSBackend.Models
{
    public class Customer : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; } = string.Empty;

        [PersonalData]
        public string LastName { get; set; } = string.Empty;

        [PersonalData]
        public string Address { get; set; }

        public List<CustomerOrder> CustomerOrders { get; set; }
    }
}
