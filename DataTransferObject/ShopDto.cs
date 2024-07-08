using WMSBackend.Models;

namespace WMSBackend.DataTransferObject
{
    public class ShopDto
    {
        public string Name { get; set; }

        public string Platform { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Website { get; set; } = string.Empty;

        public static ShopDto ToDto(Shop shop)
        {
            var outputDto = new ShopDto
            {
                Name = shop.Name,
                Platform = shop.Platform,
                Address = shop.Address,
                Website = shop.Website
            };
            return outputDto;
        }
    }
}
