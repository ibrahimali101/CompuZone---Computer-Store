using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CompuZone.BLL.AuthStuffs;
using CompuZone.BLL.DTOs.Auth;
using CompuZone.BLL.DTOs.Response;
using CompuZone.BLL.Exceptions;
using CompuZone.BLL.Services.Interfaces;
using CompuZone.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CompuZone.BLL.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtService _jwtserv;
        public AuthService(UserManager<User> usermanager, RoleManager<IdentityRole> rolemanager, IJwtService jwtService)
        {
            _userManager = usermanager;
            _roleManager = rolemanager;
            _jwtserv = jwtService;
        }

        public async Task<ResponseDto<ResAuthDto>> RegisterAsync(RegisterDto model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new ResponseDto<ResAuthDto>
                { IsSuccess = false, Message = "Email is already registered!" };

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                    errors += $"{error.Description},";
                return new ResponseDto<ResAuthDto> { IsSuccess = false ,Message = errors };
            }

            // 2. Assign Role to User
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.UserName),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var token = _jwtserv.GenerateToken((await _userManager.GetClaimsAsync(user)).ToList());

            ResAuthDto resAuth = new ResAuthDto
            {
                UserId = user.Id,
                JwtToken = token// fill here
            };
            // 3. Return Success (or generate token immediately)
            return new ResponseDto<ResAuthDto>
            {
                IsSuccess = true,
                Message = "User Created Successfully!",
                Data = resAuth
            };
        }

        public async Task<ResponseDto<ResAuthDto>> LoginAsync(LoginDto authLoginDto)
        {
            User? user = await _userManager.FindByNameAsync(authLoginDto.UserName);

            if (user == null)
            {
                throw new NotFoundException($"No user found with the username {authLoginDto.UserName}");
            }

            bool VALID = await _userManager.CheckPasswordAsync(user, authLoginDto.Password);
            if (!VALID)
            {
                throw new BadRequestException("Password doesn't match the current username");
            }

            var claims = await _userManager.GetClaimsAsync(user);

            var token = _jwtserv.GenerateToken(claims.ToList());

            var authResponseDto = new ResAuthDto
            {
                UserId = user.Id,
                JwtToken = token
            };

            return new ResponseDto<ResAuthDto>
            {
                Data = authResponseDto,
                Message = "User logged successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<bool>> LogoutAsync(string id)
        {
            // clear cache



            return new ResponseDto<bool>
            {
                IsSuccess = true,
                Message = "User logged out successfully",
                Data = true
            };
        }
    }
}
