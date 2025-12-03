using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.DAL.Entities
{
    public class OrderItem
    {
        // remember to set a composite key
        public int OrderID { get; set; } // Foreign Key [cite: 66]
        public int ProductID { get; set; } // Foreign Key [cite: 65]

        public int Quantity { get; set; } = 0; // [cite: 68]
        public decimal Price { get; set; } // Unit price at time of purchase [cite: 67]
        public decimal Total => Price * Quantity; // Quantity * Price [cite: 69]

        // Navigation Properties
        public Order Order { get; set; } // 
        public Product Product { get; set; } //
    }
}
