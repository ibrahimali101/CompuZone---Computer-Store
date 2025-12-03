using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.BLL.DTOs.Product
{
    public class CUProductDto
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public int CategoryID { get; set; } // We only need the ID to link it
    }
}
