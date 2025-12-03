using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.BLL.DTOs.ProductImage;
using CompuZone.DAL.Entities;

namespace CompuZone.BLL.DTOs.Product
{
    public class ResProductDto
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }

        // Flattened Category Data
        public string CategoryName { get; set; }

        public List<ResProductImageDto>? Images { get; set; }
    }
}
