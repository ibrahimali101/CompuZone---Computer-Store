using CompuZone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.Application.Features.Dtos.Requests
{
    public class OrderProductRequestDto
    {
        public double ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public int ProductTotalPrice { get; set; }
        public int ProductID { get; set; }
        public int OrderID { get; set; }
    }
}
