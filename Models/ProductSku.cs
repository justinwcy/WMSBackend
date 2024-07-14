namespace WMSBackend.Models
{
    public class ProductSku
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public string Sku { get; set; }
    }
}
