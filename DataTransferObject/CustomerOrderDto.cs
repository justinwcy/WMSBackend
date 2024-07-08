using WMSBackend.Models;

namespace WMSBackend.DataTransferObject
{
    public class CustomerOrderDto
    {
        public DateTime ExpectedArrivalDate { get; set; }

        public DateTime OrderCreationDate { get; set; }

        public string OrderAddress { get; set; }

        public static CustomerOrderDto ToDto(CustomerOrder customerOrder)
        {
            var outputDto = new CustomerOrderDto
            {
                ExpectedArrivalDate = customerOrder.ExpectedArrivalDate,
                OrderCreationDate = customerOrder.OrderCreationDate,
                OrderAddress = customerOrder.OrderAddress,
            };
            return outputDto;
        }
    }
}
