using WMSBackend.Models;

namespace WMSBackend.DataTransferObject
{
    public class ProductDto
    {
        public int Sku { get; set; }

        public required string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Tag { get; set; }

        public double Weight { get; set; }

        public double Height { get; set; }

        public double Length { get; set; }

        public double Width { get; set; }

        public static ProductDto ToDto(Product product)
        {
            var outputDto = new ProductDto
            {
                Sku = product.Sku,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Tag = product.Tag,
                Weight = product.Weight,
                Height = product.Height,
                Length = product.Length,
                Width = product.Width
            };
            return outputDto;
        }
    }
}
