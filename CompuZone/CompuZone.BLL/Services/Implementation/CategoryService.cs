using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CompuZone.BLL.DTOs.Category;
using CompuZone.BLL.DTOs.Product;
using CompuZone.BLL.DTOs.Response;
using CompuZone.BLL.Services.Interfaces;
using CompuZone.DAL.Entities;
using CompuZone.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompuZone.BLL.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _crepo;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepo crep, IMapper mapper)
        {
            _crepo = crep;
            _mapper = mapper;
        }
        public async Task<ResponseDto<ResCategoryDto>> CreateAsync(ReqCategoryDto dto)
        {
            Category cate = await _crepo.AddAsync(_mapper.Map<Category>(dto));
            if (cate == null)
            {
                throw new Exception("An error occurred while creating Category");
            }

            var Cdto = _mapper.Map<Category, ResCategoryDto>(cate);

            return new ResponseDto<ResCategoryDto>
            {
                Data = Cdto,
                Message = "Agent Created Successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<bool>> DeleteAsync(int id)
        {
            bool result = await _crepo.DeleteAsync(id);

            if (!result) throw new Exception("An error occurred while deleting Category");

            return new ResponseDto<bool>
            {
                Data = result,
                Message = "Category Deleted Successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<List<ResCategoryDto>>> GetAllAsync()
        {
            var categories = await _crepo.GetAllAsync().ToListAsync();
            var Cdto = _mapper.Map<List<Category> , List<ResCategoryDto>>(categories).ToList();



            return new ResponseDto<List<ResCategoryDto>>
            {
                Data = Cdto,
                Message = "Categories Retrieved Successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<ResCategoryDto>> GetByIdAsync(int id)
        {
            var category = await _crepo.GetByIdAsync(id);

            if (category == null) throw new Exception("Category not found");

            var Cdto = _mapper.Map<Category, ResCategoryDto>(category);

            return new ResponseDto<ResCategoryDto>
            {
                Data = Cdto,
                Message = "Category Retrieved Successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<bool>> UpdateAsync(int id, ReqCategoryDto dto)
        {

            var cate = _mapper.Map<ReqCategoryDto, Category>(dto);
            cate.CategoryID = id;

            bool result = await _crepo.UpdateAsync(cate);

            if (!result) throw new Exception("An error occurred while updating Category");

            return new ResponseDto<bool>
            {
                Data = result,
                Message = "Category Updated Successfully",
                IsSuccess = true
            };
        }
    }
}
