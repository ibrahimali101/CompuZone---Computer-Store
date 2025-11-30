using CompuZone.Application.Features.Dtos.Responses.CategoryResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.Application.Features.Dtos.Responses.ProductResponses
{
    public class ProductReadReponseDto
    {
        public int ID { get; set; }
        public bool IsArchived { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }

        public double BuyPrice { get; set; }
        public double SalePrice { get; set; }
        public double Quantity { get; set; }
        public int MinQuantity { get; set; }

        public CategoryReadReponseDto? Category { get; set; }
    }
}
