using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.BLL.DTOs.Customer;
using CompuZone.BLL.DTOs.Response;

namespace CompuZone.BLL.Services.Interfaces
{
    public interface ICustomerService
    {
        public Task<ResponseDto<List<ResCustomerDto>>> GetAllAsync();
        public Task<ResponseDto<ResCustomerDto>> CreateAsync(ReqCustomerDto dto);
        public Task<ResponseDto<ResCustomerDto>> GetByIdAsync(int id);
        public Task<ResponseDto<bool>> UpdateAsync(int id, ReqCustomerDto dto);
        public Task<ResponseDto<bool>> DeleteAsync(int id);
    }
}
