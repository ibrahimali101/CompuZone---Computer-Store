using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.BLL.DTOs;
using CompuZone.BLL.DTOs.Category;
using CompuZone.BLL.DTOs.Response;
using CompuZone.DAL.Entities;

namespace CompuZone.BLL.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<ResponseDto<List<ResCategoryDto>>> GetAllAsync();
        public Task<ResponseDto<ResCategoryDto>> CreateAsync(ReqCategoryDto dto);
        public Task<ResponseDto<bool>> UpdateAsync(ReqCategoryDto dto);
        public Task<ResponseDto<bool>> DeleteAsync(int id);
    }
}
