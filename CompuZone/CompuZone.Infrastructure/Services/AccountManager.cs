using CompuZone.Domain;
using CompuZone.Domain.AccountsDtos;
using CompuZone.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using CompuZone.Domain;
using CompuZone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.Infrastructure.Services
{
    public class AccountManager : IAccountManager
    {
        private readonly UserManager<ApplicaitonUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AccountManager(
            UserManager<ApplicaitonUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
            {
                _userManager = userManager;
                _roleManager = roleManager;
                _configuration = configuration;
            }

        public async Task<bool> AssignRoleToUser(AssignRoleDto assignRoleDto)
        {
            var user = await _userManager.FindByIdAsync(assignRoleDto.UserId);
            var role = await _roleManager.FindByIdAsync(assignRoleDto.RoleId);

            if (user == null || role == null)
                return false;

            var result = await _userManager.AddToRoleAsync(user, role.Name);
            if (result.Succeeded)
            {
                var claim = new Claim(ClaimTypes.Role, role.Name);
                await _userManager.AddClaimAsync(user, claim);

                return true;
            }
            return false;
        }

        public async Task<bool> CreateRole(CreateRoleDto createRoleDto)
        {
            IdentityRole identityRole = new IdentityRole()
            {
                Name = createRoleDto.RoleName,
                NormalizedName = createRoleDto.RoleName.ToUpper()
            };

            var result = await _roleManager.CreateAsync(identityRole);

            if (result.Succeeded)
                return true;

            return false;
        }

        public async Task<bool> DeleteAsync(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            var isDeleted = await _userManager.DeleteAsync(user);

            if (isDeleted.Succeeded)
                return true;

            return false;
        }

        public async Task<bool> DeleteRole(string Id)
        {
            var role = await _roleManager.FindByIdAsync(Id);

            if (role == null)
                return false;

            var isDeleted = await _roleManager.DeleteAsync(role);

            if (isDeleted.Succeeded)
                return true;

            return false;
        }

        public async Task<IEnumerable<RoleReadDto>> GetAllRoles()
        {
            var roles = await _roleManager.Roles
                .Select(a => new RoleReadDto
                {
                    Id = a.Id,
                    Name = a.Name
                })
                .ToListAsync();

            return roles;
        }

        public async Task<IEnumerable<UserReadDto>> GetAllUsers()
        {
            var users = await _userManager.Users
                .Select(a => new UserReadDto
                {
                    Id = a.Id,
                    Name = a.UserName,
                    Email = a.Email
                })
                .ToListAsync();

            return users;
        }

        public async Task<AuthUserResponseDto> LoginAsync(LoginDto loginDto)
        {
            ApplicaitonUser user = null;

            // 1. Smart Lookup: Check by Email first if input contains '@'
            if (loginDto.UserName.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(loginDto.UserName);
            }

            // 2. Fallback: Check by Username if not found by email
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(loginDto.UserName);
            }

            // Account does not exist
            if (user == null)
                return null;

            // 3. Verify Password
            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return null;

            // 4. Get Existing Claims (Roles, etc.) directly from DB
            // We are NO LONGER adding/removing the Language claim here.
            var userClaims = await _userManager.GetClaimsAsync(user);

            // 5. Generate Response
            return new AuthUserResponseDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                // We just pass the language back to the UI, but we don't save it in the DB.
                Token = GenerateToken(userClaims.ToList())
            };
        }

        public async Task<string> RegisterAsync(RegisterDto registerDto)
        {
            ApplicaitonUser user = new ApplicaitonUser();
            user.Email = registerDto.Email;
            user.UserName = registerDto.UserName;
            user.PasswordHash = registerDto.Password;

            var IdentityResult = await _userManager.CreateAsync(user, registerDto.Password);

            if (IdentityResult.Succeeded)
            {

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName)
                };

                await _userManager.AddClaimsAsync(user, claims);

                return "User Registered Successfully";
            }
            return null;
        }

        public async Task<bool> UpdateAsync(UpdateAccountDto updateAccountDto)
        {
            var user = await _userManager.FindByIdAsync(updateAccountDto.Id);

            if (user == null)
                return false;

            user.Email = updateAccountDto.Email;
            user.UserName = updateAccountDto.UserName;
            //user.PasswordHash = PasswordHasher.HashPassword(user, updateAccountDto.Password);

            var isUpdated = await _userManager.UpdateAsync(user);

            if (isUpdated.Succeeded)
                return true;

            return false;
        }

        public async Task<bool> UpdateRole(UpdateRoleDto updateRoleDto)
        {
            var role = await _roleManager.FindByIdAsync(updateRoleDto.Id);

            if (role == null)
                return false;

            role.Name = updateRoleDto.RoleName;

            var isUpdated = await _roleManager.UpdateAsync(role);

            if (isUpdated.Succeeded)
                return true;

            return false;
        }

        private string GenerateToken(List<Claim> claims)
        {
            //Security Key , HasingAlgorithm
            string secutirykey = _configuration.GetSection("SecurityKey").Value;

            var securityKeyByte = Encoding.ASCII.GetBytes(secutirykey);

            SecurityKey securityKey = new SymmetricSecurityKey(securityKeyByte);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: signingCredentials
                );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(jwtSecurityToken);
            return token;
        }
    }
}
