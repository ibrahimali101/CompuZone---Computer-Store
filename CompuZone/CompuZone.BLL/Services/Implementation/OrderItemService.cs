using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CompuZone.BLL.DTOs;
using CompuZone.BLL.DTOs.Order;
using CompuZone.BLL.DTOs.Pagination;
using CompuZone.BLL.DTOs.Response;
using CompuZone.BLL.Services.Interfaces;
using CompuZone.DAL.Entities;
using CompuZone.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompuZone.BLL.Services.Implementation
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IMapper _mapper;
        private readonly IOrderItemRepo _irepo;
        public OrderItemService(IOrderItemRepo repo, IMapper mapper)
        {
            _mapper = mapper;
            _irepo = repo;
        }
        public async Task<ResponseDto<ResOrderItemDto>> CreateAsync(ReqOrderItemDto dto)
        {
            var orderitem = _mapper.Map<ReqOrderItemDto, OrderItem>(dto);
            OrderItem oi = await _irepo.AddAsync(orderitem);
            if (oi == null) throw new Exception("an error occurred while creating an order item");

            var OIdto = _mapper.Map<OrderItem, ResOrderItemDto>(oi);

            return new ResponseDto<ResOrderItemDto>
            {
                Data = OIdto,
                Message = "Order Item Created Successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<bool>> DeleteAsync(int orderid, int productid)
        {
            bool result = await _irepo.DeleteAsync(orderid, productid);

            if (!result) throw new Exception("An error occurred while deleting Order Item");

            return new ResponseDto<bool>
            {
                Data = result,
                Message = "Order Item Deleted Successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<PagedList<ResOrderItemDto>>> GetAllAsync(PaginationParams pParams)
        {
            var pagedEntities = await _irepo.GetPagedAsync(pParams);

            var orderDtos = _mapper.Map<List<ResOrderItemDto>>(pagedEntities.Items);

            var pagedResult = new PagedList<ResOrderItemDto>(
                orderDtos,
                pagedEntities.TotalCount,
                pagedEntities.CurrentPage,
                pagedEntities.PageSize
            );

            return new ResponseDto<PagedList<ResOrderItemDto>>
            {
                Data = pagedResult,
                Message = "OrderItems Retrieved Successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<ResOrderItemDto>> GetByIdAsync(int orderid, int productid)
        {
            var orderitem = await _irepo.GetByIdAsync(orderid, productid);
            if (orderitem == null) throw new Exception("Order Item not found");
            var orderitemDto = _mapper.Map<OrderItem, ResOrderItemDto>(orderitem);
            return new ResponseDto<ResOrderItemDto>
            {
                Data = orderitemDto,
                Message = "Order Item Retrieved Successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<bool>> UpdateAsync(int orderid, ReqOrderItemDto dto)
        {
            var orderitem = _mapper.Map<ReqOrderItemDto, OrderItem>(dto);

            orderitem.OrderID = orderid;

            var result = await _irepo.UpdateAsync(orderitem);

            if (result == null) throw new Exception("An error occurred while updating Order Item");

            return new ResponseDto<bool>
            {
                Data = result,
                Message = "Order Item Updated Successfully",
                IsSuccess = true
            };
        }
    }
}
