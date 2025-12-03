using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.DAL.Entities
{
    public class Payment
    {
        public int PaymentID { get; set; } // 
        public decimal Amount { get; set; } // [cite: 5]
        public string PaymentMethod { get; set; } // e.g., Credit Card, PayPal [cite: 2]
        public DateTime TransactionDate { get; set; } // [cite: 6]
        public int OrderID { get; set; } // Foreign Key [cite: 3]

        // Navigation Properties
        public Order Order { get; set; } // Payment belongs to 1 Order
    }
}
