using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.BLL.DTOs.Auth
{
    public class ResAuthDto
    {
        public required string UserId { get; set; }
        public required string JwtToken { get; set; }
    }
}
