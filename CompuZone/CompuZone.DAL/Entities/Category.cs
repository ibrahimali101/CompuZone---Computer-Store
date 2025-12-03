using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.DAL.Entities
{
    public class Category
    {
        public int CategoryID { get; set; } // [cite: 48]
        public string CategoryName { get; set; } // [cite: 47]

        // Navigation Properties
        public ICollection<Product>? Products { get; set; } // Category contains N Products [cite: 55]
    }
}
