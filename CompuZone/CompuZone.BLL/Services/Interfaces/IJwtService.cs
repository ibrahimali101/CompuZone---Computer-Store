using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CompuZone.BLL.DTOs.Response;
using CompuZone.DAL.Entities;

namespace CompuZone.BLL.Services.Interfaces
{
    public interface IJwtService
    {
        public string GenerateToken(List<Claim> claims);
    }
}
