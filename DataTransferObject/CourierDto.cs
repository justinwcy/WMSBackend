using WMSBackend.Models;

namespace WMSBackend.DataTransferObject
{
    public class CourierDto
    {
        public string Name { get; set; }

        public Decimal Price { get; set; }

        public string Remark { get; set; }

        public static CourierDto ToDto(Courier courier)
        {
            var outputDto = new CourierDto
            {
                Name = courier.Name,
                Price = courier.Price,
                Remark = courier.Remark
            };
            return outputDto;
        }
    }
}
