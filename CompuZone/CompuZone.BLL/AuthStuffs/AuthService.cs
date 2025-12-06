using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.BLL.DTOs.Auth;
using CompuZone.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace CompuZone.BLL.AuthStuffs
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public Task<string> AddRoleAsync(AssignRole model)
        {
            throw new NotImplementedException();
        }

        public Task<AuthModel> GetTokenAsync(LoginDto model)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthModel> RegisterAsync(RegisterDto model)
        {
            // 1. Check if user exists
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthModel { Message = "Email is already registered!" };

            // 2. Create User
            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                    errors += $"{error.Description},";
                return new AuthModel { Message = errors };
            }

            // 3. Return Success (or generate token immediately)
            return new AuthModel
            {
                Message = "User Created Successfully!",
                IsAuthenticated = true,
                Username = user.UserName,
                Email = user.Email
                // You can also generate the token here if you want auto-login
            };
        }
    }
}
