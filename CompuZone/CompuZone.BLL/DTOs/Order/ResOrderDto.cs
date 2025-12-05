using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.BLL.DTOs.Customer;
using CompuZone.BLL.DTOs.Payment;
using CompuZone.BLL.DTOs.Shipping;
using CompuZone.DAL.Entities;

namespace CompuZone.BLL.DTOs.Order
{
    public class ResOrderDto // can be visible to customers
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } // Could map int to string here (e.g. "Pending")

        public string CustomerName { get; set; } // Flattened
        public ICollection<ReqOrderItemDto> OrderItems { get; set; }
        public ReqShippingDto Shipping { get; set; }
        public ReqPaymentDto Payment { get; set; }

    }
}
