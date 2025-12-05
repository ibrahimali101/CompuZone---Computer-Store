using CompuZone.BLL.DTOs.Product;

namespace CompuZone.BLL.DTOs.Category
{
    public class ResCategoryDto
    {
        public int CategoryID { get; set; } // [cite: 48]
        public string CategoryName { get; set; } // [cite: 47]

        // Navigation Properties
        public ICollection<ResProductDto>? Products { get; set; } // Category contains N Products [cite: 55]
    }
}
