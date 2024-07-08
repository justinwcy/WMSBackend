using WMSBackend.Models;

namespace WMSBackend.DataTransferObject
{
    public class IncomingOrderProductDto
    {
        public int IncomingOrderId { get; set; }

        public int ProductId { get; set; }

        public static IncomingOrderProductDto ToDto(IncomingOrderProduct incomingOrderProduct)
        {
            var outputDto = new IncomingOrderProductDto
            {
                IncomingOrderId = incomingOrderProduct.IncomingOrderId,
                ProductId = incomingOrderProduct.ProductId,
            };
            return outputDto;
        }
    }
}
