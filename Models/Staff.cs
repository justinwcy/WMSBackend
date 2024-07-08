using Microsoft.AspNetCore.Identity;

namespace WMSBackend.Models
{
    public class Staff : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; } = string.Empty;

        [PersonalData]
        public string LastName { get; set; } = string.Empty;

        // one to many relationship
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        // many to many relationship
        public List<Zone>? Zones { get; set; }
    }
}
