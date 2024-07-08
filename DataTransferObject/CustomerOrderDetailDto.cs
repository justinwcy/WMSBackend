using WMSBackend.Models;

namespace WMSBackend.DataTransferObject
{
    public class CustomerOrderDetailDto
    {
        public int Quantity { get; set; }

        public string Status { get; set; }

        public string? OrderBin { get; set; }

        public static CustomerOrderDetailDto ToDto(CustomerOrderDetail customerOrderDetail)
        {
            var outputDto = new CustomerOrderDetailDto
            {
                Quantity = customerOrderDetail.Quantity,
                Status = customerOrderDetail.Status,
                OrderBin = customerOrderDetail.OrderBin
            };
            return outputDto;
        }
    }
}
