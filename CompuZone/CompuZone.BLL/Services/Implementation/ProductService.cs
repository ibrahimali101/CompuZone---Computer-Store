using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public ProductService(IProductRepo repository)
        {
            _prepo = repository;
        }

        public async Task<ResponseDto<List<ResProductDto>>> GetAllAsync()
        {
            var products =  _prepo.GetAllAsync();

            // MANUAL MAPPING (Entity -> DTO)
            // Later, you can use AutoMapper to make this cleaner.
            List<ResProductDto> result = await products.Select(p => new ResProductDto // explain why are you waiting
            {
                ProductName = p.ProductName,
                Price = p.Price,
                Description = p.Description,
                CategoryName = p.Category == null ??, // Null check
                QuantityInStock = p.QuantityInStock
            }).ToListAsync();

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
            Product Exist = await _prepo.GetByIdAsync(dto.ProductID);
            if (Exist != null) 
            {
                return new ResponseDto<ResProductDto>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "Product already exists."
                };
            }
            Product Map = new Product
            {
                ProductName = dto.ProductName,
                Price = dto.Price,
                Description = dto.Description,
                QuantityInStock = dto.QuantityInStock,
                Images = dto.Images,
                CategoryID = dto.CategoryID ?? null,
                Category = dto.Category
            };
            Product? prod = await _prepo.AddAsync(Map);
            // Fisrt, we should map the CUProductDto to Product entity, after that we can call the repository to add the productDto.

            ResProductDto prodDTO = new ResProductDto
            {
                ProductName = dto.ProductName,
                Description = dto.Description,
                QuantityInStock = dto.QuantityInStock,
                Images = dto.Images,
                CategoryName = dto.Category == null ? "UnCategorized" : dto.Category.CategoryName
            };

            return new ResponseDto<ResProductDto>
            {
                Data = prodDTO,
                IsSuccess = true,
                Message = "Products Created successfully."
            };
        }
        public async Task<ResponseDto<bool>> UpdateAsync(ReqProductDto dto)
        {
            var product = await _prepo.GetByIdAsync(dto.ProductID);
            if (product == null)
            {
                throw new NotFoundException($"No agent found with ID {dto.ProductID}");
            }
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
    }
}
