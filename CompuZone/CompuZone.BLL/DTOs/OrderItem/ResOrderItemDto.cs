using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.BLL.DTOs
{
    public class ResOrderItemDto
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } // Flattened from Product entity
        public decimal Price { get; set; } // Unit Price snapshot
        public int Quantity { get; set; }
        public decimal Total { get; set; } // Calculated Total
    }
}