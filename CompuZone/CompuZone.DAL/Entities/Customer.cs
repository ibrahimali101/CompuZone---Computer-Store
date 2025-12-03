using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.DAL.Entities
{
    public class Customer
    {
        public int CustomerID { get; set; } // [cite: 17]
        public string Name { get; set; } // [cite: 16]
        public string Email { get; set; } // [cite: 18]
        public string Phone { get; set; } // [cite: 20]
        public string? Address { get; set; } // [cite: 19]
        public DateTime? DateOfBirth { get; set; } // [cite: 22]

        // Navigation Properties
        public ICollection<Order>? Orders { get; set; } // 1 Customer makes N Orders [cite: 28, 34]
    }
}
