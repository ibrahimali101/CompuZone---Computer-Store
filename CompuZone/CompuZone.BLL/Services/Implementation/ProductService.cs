using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CompuZone.BLL.DTOs.Product;
using CompuZone.BLL.DTOs.Response;
using CompuZone.BLL.Exceptions;
using CompuZone.BLL.Interfaces;
using CompuZone.BLL.Services.Interfaces;
using CompuZone.DAL.Entities;
using CompuZone.DAL.Repository.Implementation;
using Microsoft.EntityFrameworkCore;

namespace CompuZone.BLL.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _prepo;
        private readonly IMapper _mapper;

        public ProductService(IProductRepo repository, IMapper mapper)
        {
            _prepo = repository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<ResProductDto>>> GetAllAsync()
        {
            var products = await _prepo.GetAllAsync().ToListAsync();
            // mapping
            var result = _mapper.Map<List<Product>, List<ResProductDto>>(products);

            return new ResponseDto<List<ResProductDto>>
            {
                Data = result,
                IsSuccess = true,
                Message = "Products retrieved successfully."
            };
        }

        public async Task<ResponseDto<ResProductDto>> CreateAsync(ReqProductDto dto)
        {
            // Ibrahim: we should validate that the product does not exist. -- Yes! I've done it

            // 2. Map DTO -> Entity
            var product = _mapper.Map<ReqProductDto, Product>(dto);

            product = await _prepo.AddAsync(product);
            
            if (product == null)
            {
                throw new BadRequestException("An error occurred while making your request.");
            }

            // Assigning a role
            // here

            var resProd = _mapper.Map<Product, ResProductDto>(product);

            return new ResponseDto<ResProductDto>
            {
                Data = resProd,
                IsSuccess = true,
                Message = "Products Created successfully."
            };
        }
        public async Task<ResponseDto<bool>> UpdateAsync(int id, ReqProductDto dto)
        {
            var product = await _prepo.GetByIdAsync(id);
            if (product == null)
            {
                throw new NotFoundException($"No agent found with ID {id}");
            }
            // do i need this?
            // product = _mapper.Map<ReqProductDto, Product>(dto);

            bool result = await _prepo.UpdateAsync(product);
            if (!result)
            {
                throw new Exception("An error occurred while updating product");
            }
            return new ResponseDto<bool>
            {
                Message = "Product Updated Successfully",
                IsSuccess = true
            };

        }

        public async Task<ResponseDto<bool>> DeleteAsync(int id)
        {
            bool result = await _prepo.DeleteAsync(id);
            if (!result)
            {
                throw new Exception("An error occurred while deleting product");
            }
            return new ResponseDto<bool>
            {
                Message = "Product Deleted Successfully",
                IsSuccess = result
            };
        }

        public async Task<ResponseDto<ResProductDto>> GetByIdAsync(int id)
        {
            var product = await _prepo.GetByIdAsync(id);
            if (product == null) 
            {
                throw new NotFoundException($"No product found with ID {id}");
            }
            var resProd = _mapper.Map<Product, ResProductDto>(product);

            return new ResponseDto<ResProductDto>
            {
                Data = resProd,
                IsSuccess = true,
                Message = "Product retrieved successfully."
            };
        }
    }
}
