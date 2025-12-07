using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.DAL.Entities
{
    public class Customer
    {
        public int CustomerID { get; set; } 
        public string Name { get; set; }
        public string Email { get; set; } 
        public string Phone { get; set; } 
        public string Address { get; set; } 
        public string UserID { get; set; }
        // Navigation Properties
        public User User { get; set; }
        public ICollection<Order>? Orders { get; set; } 
    }
}
