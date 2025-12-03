using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.BLL.DTOs.Order;
using CompuZone.BLL.DTOs.Product;

namespace CompuZone.BLL.DTOs.Category
{
    public class ResCategoryDto
    {
        public string CategoryName { get; set; } // [cite: 47]
        public ICollection<ResProductDto>? Products { get; set; } // Category contains N Products [cite: 55]
    }
}
