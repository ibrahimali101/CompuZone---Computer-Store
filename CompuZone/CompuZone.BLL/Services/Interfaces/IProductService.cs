using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.BLL.DTOs.Order;
using CompuZone.BLL.DTOs.Pagination;
using CompuZone.BLL.DTOs.Product;
using CompuZone.BLL.DTOs.Response;
using CompuZone.BLL.Interfaces;
using CompuZone.DAL.Entities;

namespace CompuZone.BLL.Services.Interfaces
{
    public interface IProductService
    {
        public Task<ResponseDto<PagedList<ResProductDto>>> GetAllAsync(PaginationParams pParams);
        public Task<ResponseDto<ResProductDto>> CreateAsync(ReqProductDto dto);
        public Task<ResponseDto<ResProductDto>> GetByIdAsync(int id);
        public Task<ResponseDto<bool>> UpdateAsync(int id, ReqProductDto dto);
        public Task<ResponseDto<bool>> DeleteAsync(int id);
    }
}
