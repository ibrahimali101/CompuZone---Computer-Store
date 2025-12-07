using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CompuZone.BLL.DTOs.Response;
using CompuZone.BLL.Services.Interfaces;
using CompuZone.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CompuZone.BLL.Services.Implementation
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        public JwtService(IConfiguration configuration, UserManager<User> usermanager)
        {
            _configuration = configuration;
            _userManager = usermanager;
        }
        public string GenerateToken(List<Claim> claims)
        {
            string secKey = _configuration.GetSection("JWT").GetSection("Key").Value;

            var secbyte = Encoding.ASCII.GetBytes(secKey);

            SecurityKey securityKey = new SymmetricSecurityKey(secbyte);

            SigningCredentials signCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signCred
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
