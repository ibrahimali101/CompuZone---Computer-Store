using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.BLL.DTOs.Product;

namespace CompuZone.BLL.DTOs
{
    public class ReqOrderItemDto
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
