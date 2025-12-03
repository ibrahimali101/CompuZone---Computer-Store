using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.BLL.DTOs.Payment
{
    public class ReqPaymentDto
    {
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
    }
}
