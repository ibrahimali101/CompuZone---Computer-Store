using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.DAL.Entities
{
    public class Shipping
    {
        public int ShippingID { get; set; } // [cite: 8]
        public string Address { get; set; } // [cite: 11]
        public string TrackingNumber { get; set; } // [cite: 37]
        public string ShippingStatus { get; set; } // [cite: 38]
        public DateTime EstimatedDeliveryDate { get; set; } // [cite: 42]
        public DateTime? ActualDeliveryDate { get; set; } // Nullable if not delivered yet [cite: 42]
        public int OrderID { get; set; } // Foreign Key [cite: 9]
        public int CustomerID { get; set; } // Foreign Key (for quick lookup) [cite: 7]

        // Navigation Properties
        public Order Order { get; set; } //
    }
}
