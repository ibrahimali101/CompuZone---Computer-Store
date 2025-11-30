using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.Application.Features.Dtos.Responses.CustomerResponses
{
    public class CustomerReadReponseDto
    {
        public int ID { get; set; }
        public bool IsArchived { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
