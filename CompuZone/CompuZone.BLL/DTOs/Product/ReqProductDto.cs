using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.BLL.DTOs.ProductImage;
using CompuZone.DAL.Entities;

namespace CompuZone.BLL.DTOs.Product
{
    public class ReqProductDto
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public int? CategoryID { get; set; } // Link to category

        // Optional: Allow uploading images while creating product
        public List<ResProductImageDto>? Images { get; set; }
    }
}
