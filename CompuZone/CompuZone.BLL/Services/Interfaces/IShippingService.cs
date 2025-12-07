using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.BLL.DTOs.Order;
using CompuZone.BLL.DTOs.Pagination;
using CompuZone.BLL.DTOs.Response;
using CompuZone.BLL.DTOs.Shipping;
using CompuZone.DAL.Entities;

namespace CompuZone.BLL.Services.Interfaces
{
    public interface IShippingService
    {
        public Task<ResponseDto<PagedList<ResShippingDto>>> GetAllAsync(PaginationParams pParams);
        public Task<ResponseDto<ResShippingDto>> CreateAsync(ReqShippingDto dto);
        public Task<ResponseDto<ResShippingDto>> GetByIdAsync(int id);
        public Task<ResponseDto<bool>> UpdateAsync(int id, ReqShippingDto dto);
        public Task<ResponseDto<bool>> DeleteAsync(int id);
    }
}
