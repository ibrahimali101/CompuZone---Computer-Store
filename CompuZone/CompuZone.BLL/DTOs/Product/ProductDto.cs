using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.BLL.DTOs.Product
{
    public class ProductDto
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsInStock { get; set; } // Calculated field (QuantityInStock > 0)
        public string CategoryName { get; set; } // Flattened from relation
    }
}
