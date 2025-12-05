using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CompuZone.BLL.DTOs.Response;
using CompuZone.BLL.DTOs.Shipping;
using CompuZone.BLL.Services.Interfaces;
using CompuZone.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompuZone.BLL.Services.Implementation
{
    public class ShippingService : IShippingService
    {
        private readonly IMapper _mapper;
        private readonly IShippingRepo _shrepo;
        public ShippingService(IMapper mapper, IShippingRepo shrepo)
        {
            _mapper = mapper;
            _shrepo = shrepo;
        }

        public async Task<ResponseDto<ResShippingDto>> CreateAsync(ReqShippingDto dto)
        {
            var shipping = _mapper.Map<ReqShippingDto, DAL.Entities.Shipping>(dto);
            var sh = await _shrepo.AddAsync(shipping);
            if (sh == null) throw new Exception("an error occurred while creating a shipping record");
            var Shdto = _mapper.Map<DAL.Entities.Shipping, ResShippingDto>(sh);
            return new ResponseDto<ResShippingDto>
            {
                Data = Shdto,
                Message = "Shipping Record Created Successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<bool>> DeleteAsync(int id)
        {
            bool result = await _shrepo.DeleteAsync(id);
            if (!result) throw new Exception("An error occurred while deleting Shipping Record");
            return new ResponseDto<bool>
            {
                Data = result,
                Message = "Shipping Record Deleted Successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<List<ResShippingDto>>> GetAllAsync()
        {
            var shippings = await _shrepo.GetAllAsync().ToListAsync();
            var Shdto = _mapper.Map<List<DAL.Entities.Shipping>, List<ResShippingDto>>(shippings);
            return new ResponseDto<List<ResShippingDto>>
            {
                Data = Shdto,
                Message = "Shipping Records Retrieved Successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<ResShippingDto>> GetByIdAsync(int id)
        {
            var shipping = await _shrepo.GetByIdAsync(id);
            if (shipping == null) throw new Exception("Shipping Record not found");
            var Shdto = _mapper.Map<DAL.Entities.Shipping, ResShippingDto>(shipping);
            return new ResponseDto<ResShippingDto>
            {
                Data = Shdto,
                Message = "Shipping Record Retrieved Successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<bool>> UpdateAsync(int id, ReqShippingDto dto)
        {
            var shipping = _mapper.Map<ReqShippingDto, DAL.Entities.Shipping>(dto);
            var result = await _shrepo.UpdateAsync(shipping);
            if (!result) throw new Exception("An error occurred while updating Shipping Record");
            return new ResponseDto<bool>
            {
                Data = result,
                Message = "Shipping Record Updated Successfully",
                IsSuccess = true
            };
        }
    }
}
