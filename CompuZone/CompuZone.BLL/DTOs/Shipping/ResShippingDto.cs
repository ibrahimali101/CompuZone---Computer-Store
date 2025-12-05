using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.BLL.DTOs.Shipping
{
    public class ResShippingDto
    {
        public int ShippingID { get; set; }
        public string Address { get; set; }
        public string TrackingNumber { get; set; }
        public string ShippingStatus { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public DateTime? ActualDeliveryDate { get; set; }
    }
}
