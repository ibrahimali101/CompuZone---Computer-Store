using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.BLL.DTOs.Product;
using CompuZone.BLL.DTOs.Response;
using CompuZone.BLL.Interfaces;

namespace CompuZone.BLL.Services.Interfaces
{
    public interface IProductService
    {
        public Task<ResponseDto<List<ResProductDto>>> GetAllAsync();
        public Task<ResponseDto<ResProductDto>> CreateAsync(ReqProductDto dto);
        public Task<ResponseDto<bool>> UpdateAsync(ReqProductDto dto);
        public Task<ResponseDto<bool>> DeleteAsync(int id);
    }
}
