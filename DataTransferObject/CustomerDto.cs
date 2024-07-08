using Microsoft.AspNetCore.Identity;
using WMSBackend.Models;

namespace WMSBackend.DataTransferObject
{
    public class CustomerDto
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public static CustomerDto ToDto(Customer customer)
        {
            var outputDto = new CustomerDto
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                UserName = customer.UserName,
                Email = customer.Email,
                Address = customer.Address
            };
            return outputDto;
        }
    }
}
