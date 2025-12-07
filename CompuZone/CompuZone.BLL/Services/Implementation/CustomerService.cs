using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CompuZone.BLL.DTOs.Customer;
using CompuZone.BLL.DTOs.Pagination;
using CompuZone.BLL.DTOs.Response;
using CompuZone.BLL.Exceptions;
using CompuZone.BLL.Services.Interfaces;
using CompuZone.DAL.Entities;
using CompuZone.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompuZone.BLL.Services.Implementation
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _crepo;
        private readonly IMapper _mapper;
        public CustomerService(ICustomerRepo crepo, IMapper mapper)
        {
            _mapper = mapper;
            _crepo = crepo;
        }
        public async Task<ResponseDto<ResCustomerDto>> CreateAsync(ReqCustomerDto dto)
        {
            Customer cust = await _crepo.AddAsync(_mapper.Map<ReqCustomerDto, Customer>(dto));

            if (cust == null) throw new BadRequestException("an error occurred while creating a customer");

            var Cdto = _mapper.Map<Customer, ResCustomerDto>(cust);
            return new ResponseDto<ResCustomerDto>
            {
                Data = Cdto,
                Message = "Customer Created Successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<bool>> DeleteAsync(int id)
        {
            bool result = await _crepo.DeleteAsync(id);

            if (!result) throw new Exception("An error occurred while deleting Customer");

            return new ResponseDto<bool>
            {
                Data = result,
                Message = "Customer Deleted Successfully",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<PagedList<ResCustomerDto>>> GetAllAsync(PaginationParams pParams)
        {
            var pagedEntities = await _crepo.GetPagedAsync(pParams);

            var customerDtos = _mapper.Map<List<ResCustomerDto>>(pagedEntities.Items);

            var pagedResult = new PagedList<ResCustomerDto>(
                customerDtos,
                pagedEntities.TotalCount,
                pagedEntities.CurrentPage,
                pagedEntities.PageSize
            );

            return new ResponseDto<PagedList<ResCustomerDto>>
            {
                Data = pagedResult,
                Message = "Customers retrieved successfully.",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<ResCustomerDto>> GetByIdAsync(int id)
        {
            Customer cust = await _crepo.GetByIdAsync(id);

            if (cust == null) throw new NotFoundException("Customer not found");

            ResCustomerDto Cdto = _mapper.Map<Customer, ResCustomerDto>(cust);
            return new ResponseDto<ResCustomerDto>
            {
                Data = Cdto,
                Message = "Customer retrieved successfully.",
                IsSuccess = true
            };
        }

        public async Task<ResponseDto<bool>> UpdateAsync(int id, ReqCustomerDto dto)
        {
            Customer customer = _mapper.Map<ReqCustomerDto, Customer>(dto);
            customer.CustomerID = id;

            if (await _crepo.GetByIdAsync(id) == null) throw new NotFoundException("Customer not found");

            bool result = await _crepo.UpdateAsync(customer);

            if (!result) throw new Exception("An error occurred while updating Customer");

            return new ResponseDto<bool>
            {
                Data = result,
                Message = "Customer Updated Successfully",
                IsSuccess = true
            };
        }
    }
}
