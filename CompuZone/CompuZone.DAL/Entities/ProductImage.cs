using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.DAL.Entities
{
    public class ProductImage
    {
        public int ImageID { get; set; } // [cite: 75]
        public string ImageURL { get; set; } // [cite: 74]
        public int ImageOrder { get; set; } // [cite: 77]
        public int ProductID { get; set; } // Foreign Key [cite: 76]

        // Navigation Properties
        public Product? Product { get; set; } //
    }
}
