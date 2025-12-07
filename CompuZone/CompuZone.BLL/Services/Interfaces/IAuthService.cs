using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.BLL.DTOs.Auth;
using CompuZone.BLL.DTOs.Response;

namespace CompuZone.BLL.AuthStuffs
{
    public interface IAuthService
    {
        public Task<ResponseDto<ResAuthDto>> RegisterAsync(RegisterDto authRegisterDto);
        public Task<ResponseDto<ResAuthDto>> LoginAsync(LoginDto authLoginDto);
        public Task<ResponseDto<bool>> LogoutAsync(string id);
    }
}
