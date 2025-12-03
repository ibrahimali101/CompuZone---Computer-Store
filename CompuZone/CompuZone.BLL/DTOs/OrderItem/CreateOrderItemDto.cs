using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.BLL.DTOs
{
    public class CreateOrderItemDto
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
