using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.BLL.DTOs.Payment;
using CompuZone.BLL.DTOs.Shipping;

namespace CompuZone.BLL.DTOs.Order
{
    public class OrderDetailDto
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }

        // Nested DTOs for structure
        public string CustomerName { get; set; }
        public ShippingDto ShippingDetails { get; set; }
        public PaymentDto PaymentDetails { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
}
