using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.BLL.DTOs;
using CompuZone.BLL.DTOs.Response;
using CompuZone.DAL.Entities;

namespace CompuZone.BLL.Services.Interfaces
{
    public interface IOrderItemService
    {
        public Task<ResponseDto<List<ResOrderItemDto>>> GetAllAsync();
        public Task<ResponseDto<ResOrderItemDto>> GetByIdAsync(int orderid, int productid);
        public Task<ResponseDto<ResOrderItemDto>> CreateAsync(ReqOrderItemDto dto);
        public Task<ResponseDto<bool>> UpdateAsync(int orderid, ReqOrderItemDto dto);
        public Task<ResponseDto<bool>> DeleteAsync(int orderid, int productid);
    }
}
