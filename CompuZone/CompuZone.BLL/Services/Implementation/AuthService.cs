using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public AuthService(IMapper mapper, UserManager<User> usermanager, RoleManager<IdentityRole> rolemanager, IJwtService jwtService)
        {
            _userManager = usermanager;
            _roleManager = rolemanager;
            _jwtserv = jwtService;
            _mapper = mapper;
        }

        public async Task<ResponseDto<ResAuthDto>> RegisterAsync(RegisterDto model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new ResponseDto<ResAuthDto>
                { IsSuccess = false, Message = "Email is already registered!" };

            User _user = _mapper.Map<RegisterDto, User>(model);
            _user.PasswordHash = new PasswordHasher<User>().HashPassword(_user, model.Password);
            var result = await _userManager.CreateAsync(_user);

            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                    errors += $"{error.Description},";
                return new ResponseDto<ResAuthDto> { IsSuccess = false ,Message = errors };
            }

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user.UserName),
                new Claim(ClaimTypes.Email, _user.Email),
                new Claim(ClaimTypes.NameIdentifier, _user.Id),
                new Claim(ClaimTypes.Role, model.Role == 0 ? "Admin" : "Customer")
            };

            var iden = await _userManager.AddClaimsAsync(_user, claims);

            var token = _jwtserv.GenerateToken((await _userManager.GetClaimsAsync(_user)).ToList());

            ResAuthDto resAuth = new ResAuthDto
            {
                UserId = _user.Id,
                JwtToken = token// fill here
            };
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
    }
}
