using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.BLL.DTOs
{
    public class OrderItemDto
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; } // Price at moment of purchase
        public int Quantity { get; set; }
        public decimal LineTotal { get; set; } // Quantity * UnitPrice
    }
}
