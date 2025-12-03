using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.BLL.DTOs.Category;
using CompuZone.BLL.DTOs.Product;
using CompuZone.BLL.DTOs.Response;
using CompuZone.BLL.Services.Interfaces;
using CompuZone.DAL.Entities;

namespace CompuZone.BLL.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        public Task<ResponseDto<ResCategoryDto>> CreateAsync(ReqCategoryDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<bool>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<List<ResCategoryDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<bool>> UpdateAsync(ReqCategoryDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
