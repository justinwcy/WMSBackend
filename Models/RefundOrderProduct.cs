using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WMSBackend.Models
{
    public class RefundOrderProduct
    {
        public int RefundOrderId { get; set; }

        public int ProductId { get; set; }
    }
}
