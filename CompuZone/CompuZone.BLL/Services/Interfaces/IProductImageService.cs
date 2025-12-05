using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.BLL.DTOs.ProductImage;
using CompuZone.BLL.DTOs.Response;

namespace CompuZone.BLL.Services.Interfaces
{
    public interface IProductImageImageService
    {
        public Task<ResponseDto<List<ResProductImageDto>>> GetAllAsync();
        public Task<ResponseDto<ResProductImageDto>> CreateAsync(ReqProductImageDto dto);
        public Task<ResponseDto<ResProductImageDto>> GetByIdAsync(int id);
        public Task<ResponseDto<bool>> UpdateAsync(int id, ReqProductImageDto dto);
        public Task<ResponseDto<bool>> DeleteAsync(int id);
    }
}
