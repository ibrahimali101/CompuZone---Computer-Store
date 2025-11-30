using CompuZone.Application.Features.Dtos.Responses.CategoryResponses;

namespace CompuZone.Application.Features.Dtos.Responses.ProductResponses
{
    public class ProductReadReponseDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? ProductImage { get; set; }
        public bool IsArchived { get; set; }

        // Nested DTO
        public CategoryReadReponseDto Category { get; set; }
    }
}