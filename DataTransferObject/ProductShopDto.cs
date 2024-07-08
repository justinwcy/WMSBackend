using WMSBackend.Models;

namespace WMSBackend.DataTransferObject
{
    public class ProductShopDto
    {
        public int ProductId { get; set; }

        public int ShopId { get; set; }

        public static ProductShopDto ToDto(ProductShop productShop)
        {
            var outputDto = new ProductShopDto
            {
                ProductId = productShop.ProductId,
                ShopId = productShop.ShopId
            };
            return outputDto;
        }
    }
}
