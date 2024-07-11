namespace WMSBackend.DataTransferObject
{
    public class CustomerDto
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }
    }
}
