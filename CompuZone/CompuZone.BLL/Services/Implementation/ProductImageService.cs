using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CompuZone.BLL.DTOs;
using CompuZone.BLL.DTOs.Pagination;
using CompuZone.BLL.DTOs.ProductImage;
using CompuZone.BLL.DTOs.Response;
using CompuZone.BLL.Services.Interfaces;
using CompuZone.DAL.Entities;
using CompuZone.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompuZone.BLL.Services.Implementation
{
    public class ProductImageService : IProductImageService
    {
        private readonly IProductImageRepo _pirepo;
        private readonly IMapper _mapper;
        public ProductImageService(IProductImageRepo pirepo, IMapper mapper)
        {
            _pirepo = pirepo;
            _mapper = mapper;
        }

        public async Task<ResponseDto<ResProductImageDto>> CreateAsync(ReqProductImageDto dto)
        {
            ProductImage pi = await _pirepo.AddAsync(_mapper.Map<ReqProductImageDto, ProductImage>(dto));
            if (pi == null) throw new Exception("an error occurred while creating a product image");
            var Pidto = _mapper.Map<ProductImage, ResProductImageDto>(pi);
            return new ResponseDto<ResProductImageDto>
            {
                Data = Pidto,
                Message = "Product Image Created Successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<bool>> DeleteAsync(int id)
        {
            bool result = await _pirepo.DeleteAsync(id);
            if (!result) throw new Exception("An error occurred while deleting Product Image");
            return new ResponseDto<bool>
            {
                Data = result,
                Message = "Product Image Deleted Successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<PagedList<ResProductImageDto>>> GetAllAsync(PaginationParams pParams)
        {
            var pagedEntities = await _pirepo.GetPagedAsync(pParams);
            var orderDtos = _mapper.Map<List<ResProductImageDto>>(pagedEntities.Items);

            // 3. Wrap
            var pagedResult = new PagedList<ResProductImageDto>(
                orderDtos,
                pagedEntities.TotalCount,
                pagedEntities.CurrentPage,
                pagedEntities.PageSize
            );

            return new ResponseDto<PagedList<ResProductImageDto>>
            {
                Data = pagedResult,
                Message = "ProductImages Retrieved Successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<ResProductImageDto>> GetByIdAsync(int id)
        {
            ProductImage pi = await _pirepo.GetByIdAsync(id);
            if (pi == null) throw new Exception("Product Image not found");
            ResProductImageDto Pidto = _mapper.Map<ProductImage, ResProductImageDto>(pi);
            return new ResponseDto<ResProductImageDto>
            {
                Data = Pidto,
                Message = "Product Image Retrieved Successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<bool>> UpdateAsync(int id, ReqProductImageDto dto)
        {
            ProductImage pi = _mapper.Map<ReqProductImageDto, ProductImage>(dto);
            pi.ImageID = id;
            bool result = await _pirepo.UpdateAsync(pi);
            if (!result) throw new Exception("An error occurred while updating Product Image");
            return new ResponseDto<bool>
            {
                Data = result,
                Message = "Product Image Updated Successfully",
                IsSuccess = true
            };
        }
    }
}
