using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.DAL.Entities
{
    public class Order
    {
        public int OrderID { get; set; } // [cite: 39]
        public DateTime OrderDate { get; set; } // [cite: 44]
        public decimal TotalAmount { get; set; } // [cite: 43]
        public string Status { get; set; } // [cite: 41]
        public int CustomerID { get; set; } // Foreign Key [cite: 40]

        // Navigation Properties
        public Customer Customer { get; set; } // Order is made by 1 Customer 
        public ICollection<OrderItem> OrderItems { get; set; } // Order contains N Items [cite: 45]
        public Payment Payment { get; set; } // Order has 1 Payment [cite: 15]
        public Shipping Shipping { get; set; } // Order has 1 Shipping detail [cite: 13]
    }
}
