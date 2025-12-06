using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.BLL.DTOs.Auth;

namespace CompuZone.BLL.AuthStuffs
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterDto model);
        Task<AuthModel> GetTokenAsync(LoginDto model);
        Task<string> AddRoleAsync(AssignRole model);
    }
}
