using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.Application.Features.Dtos.Responses.StockResponses
{
    public class StockReadReponseDto
    {
        public int ID { get; set; }
        public bool IsArchived { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public double TotalMoney { get; set; }
    }
}
