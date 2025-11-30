using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.Domain
{
    public class PaginateBaseParamter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PaginateBaseParamter()
        {
            PageNumber = 1;
            PageSize = 10;
        }
        public PaginateBaseParamter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
