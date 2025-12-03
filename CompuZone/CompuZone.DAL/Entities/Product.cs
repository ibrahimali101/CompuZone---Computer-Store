using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.DAL.Entities
{
    public class Product
    {
        public int ProductID { get; set; } // [cite: 56]
        public string ProductName { get; set; }// [cite: 58]
        public string Description { get; set; } = "Nothing here.";// [cite: 53]
        public decimal Price { get; set; } = 0; // [cite: 59]
        public int QuantityInStock { get; set; } = 0; // [cite: 54] 
        public int? CategoryID { get; set; } // Foreign Key [cite: 57]

        public CategoryDto? Category { get; set; } // Product is in 1 Category 
        public ICollection<ProductImageDto>? Images { get; set; } // Product contains N Images [cite: 72]
        public ICollection<OrderItem>? OrderItems { get; set; } // Product is in N OrderItems [cite: 60]
    }
}
