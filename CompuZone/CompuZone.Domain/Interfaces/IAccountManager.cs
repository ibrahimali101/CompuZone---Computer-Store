using CompuZone.Domain.AccountsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.Domain
{
    public interface IAccountManager
    {
        Task<AuthUserResponseDto> LoginAsync(LoginDto loginDto);
        Task<string> RegisterAsync(RegisterDto registerDto);
        Task<bool> DeleteAsync(string Id);
        Task<bool> UpdateAsync(UpdateAccountDto updateAccountDto);
        Task<IEnumerable<UserReadDto>> GetAllUsers();

        Task<bool> CreateRole(CreateRoleDto createRoleDto);
        Task<bool> AssignRoleToUser(AssignRoleDto assignRoleDto);
        Task<bool> UpdateRole(UpdateRoleDto updateRoleDto);
        Task<bool> DeleteRole(string Id);
        Task<IEnumerable<RoleReadDto>> GetAllRoles();
    }
}
