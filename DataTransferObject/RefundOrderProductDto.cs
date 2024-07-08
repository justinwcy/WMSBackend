using WMSBackend.Models;

namespace WMSBackend.DataTransferObject
{
    public class RefundOrderProductDto
    {
        public int RefundOrderId { get; set; }

        public int ProductId { get; set; }

        public static RefundOrderProductDto ToDto(RefundOrderProduct refundOrderProduct)
        {
            var outputDto = new RefundOrderProductDto
            {
                RefundOrderId = refundOrderProduct.RefundOrderId,
                ProductId = refundOrderProduct.ProductId,
            };
            return outputDto;
        }
    }
}
