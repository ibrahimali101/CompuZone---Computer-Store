using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.BLL.DTOs.Order;
using CompuZone.BLL.DTOs.Response;

namespace CompuZone.BLL.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<ResponseDto<List<ResOrderDto>>> GetAllAsync();
        public Task<ResponseDto<ResOrderDto>> CreateAsync(ReqOrderDto dto);
        public Task<ResponseDto<ResOrderDto>> GetByIdAsync(int id);
        public Task<ResponseDto<bool>> UpdateAsync(int id, ReqOrderDto dto);
        public Task<ResponseDto<bool>> DeleteAsync(int id);
    }
}
