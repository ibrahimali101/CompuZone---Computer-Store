using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.BLL.DTOs.Order
{
    public class CreateOrderDto
    {
        public int CustomerID { get; set; }
        public string ShippingAddress { get; set; }
        public string PaymentMethod { get; set; } // e.g., "CreditCard"

        // A simplified list of what they bought
        public List<CreateOrderItemDto> Items { get; set; }
    }
}
