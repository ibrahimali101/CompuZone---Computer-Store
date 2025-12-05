using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CompuZone.BLL.DTOs.Order;
using CompuZone.BLL.DTOs.Response;
using CompuZone.BLL.Services.Interfaces;
using CompuZone.DAL.Entities;
using CompuZone.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompuZone.BLL.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _orepo;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepo orepo, IMapper mapper )
        {
            _mapper = mapper;
            _orepo = orepo; 
        }
        public async Task<ResponseDto<ResOrderDto>> CreateAsync(ReqOrderDto dto)
        {
            Order order = await _orepo.AddAsync(_mapper.Map<ReqOrderDto, Order>(dto));

            if (order == null) throw new Exception("an error occurred while creating an order");

            var Odto = _mapper.Map<Order, ResOrderDto>(order);
            return new ResponseDto<ResOrderDto>
            {
                Data = Odto,
                Message = "Order Created Successfully",
                IsSuccess = true
            };

        }

        public async Task<ResponseDto<bool>> DeleteAsync(int id)
        {
            bool result = await _orepo.DeleteAsync(id);
            if (!result) throw new Exception("An error occurred while deleting Order");
            return new ResponseDto<bool>
            {
                Data = result,
                Message = "Order Deleted Successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<List<ResOrderDto>>> GetAllAsync()
        {
            List<Order> orders = await _orepo.GetAllAsync().ToListAsync();
            List<ResOrderDto> Odto = _mapper.Map<List<Order>, List<ResOrderDto>>(orders);
            return new ResponseDto<List<ResOrderDto>>
            {
                Data = Odto,
                Message = "Orders Retrieved Successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<ResOrderDto>> GetByIdAsync(int id)
        {
            Order order =  await _orepo.GetByIdAsync(id);
            if (order == null) throw new Exception("Order not found");
            ResOrderDto Odto = _mapper.Map<Order, ResOrderDto>(order);
            return new ResponseDto<ResOrderDto>
            {
                Data = Odto,
                Message = "Order Retrieved Successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<bool>> UpdateAsync(int id, ReqOrderDto dto)
        {
            Order orderToUpdate = await _orepo.GetByIdAsync(id);
            if (orderToUpdate == null) throw new Exception("Order not found");
            _mapper.Map(dto, orderToUpdate);
            bool result = await _orepo.UpdateAsync(orderToUpdate);
            if (!result) throw new Exception("An error occurred while updating Order");
            return new ResponseDto<bool>
            {
                Data = result,
                Message = "Order Updated Successfully",
                IsSuccess = true
            };
        }
    }
}
