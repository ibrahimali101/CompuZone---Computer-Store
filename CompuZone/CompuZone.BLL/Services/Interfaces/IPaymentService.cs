using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.BLL.DTOs.Order;
using CompuZone.BLL.DTOs.Pagination;
using CompuZone.BLL.DTOs.Payment;
using CompuZone.BLL.DTOs.Response;
using CompuZone.DAL.Entities;

namespace CompuZone.BLL.Services.Interfaces
{
    public interface IPaymentService
    {
        public Task<ResponseDto<PagedList<ResPaymentDto>>> GetAllAsync(PaginationParams pParams);
        public Task<ResponseDto<ResPaymentDto>> CreateAsync(ReqPaymentDto dto);
        public Task<ResponseDto<ResPaymentDto>> GetByIdAsync(int id);
        public Task<ResponseDto<bool>> UpdateAsync(int id, ReqPaymentDto dto);
        public Task<ResponseDto<bool>> DeleteAsync(int id);
    }
}
