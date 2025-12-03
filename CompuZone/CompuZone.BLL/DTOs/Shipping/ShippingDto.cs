using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.BLL.DTOs.Shipping
{
    public class ShippingDto
    {
        public string TrackingNumber { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
        public DateTime EstimatedDelivery { get; set; }
    }
}
