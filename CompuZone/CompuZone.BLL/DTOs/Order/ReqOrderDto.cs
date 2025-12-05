using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.BLL.DTOs.Payment;
using CompuZone.BLL.DTOs.Shipping;
using CompuZone.DAL.Entities;

namespace CompuZone.BLL.DTOs.Order
{
    public class ReqOrderDto
    {
        public int CustomerID { get; set; }
        public ICollection<ReqOrderItemDto> OrderItems { get; set; }

        // We can include initial shipping/payment info here if needed
        public ReqShippingDto ShippingDetails { get; set; }
        public ReqPaymentDto PaymentDetails { get; set; }
    }
}
