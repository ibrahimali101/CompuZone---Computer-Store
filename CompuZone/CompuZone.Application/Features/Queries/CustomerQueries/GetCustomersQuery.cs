using AutoMapper;
using MediatR;
using CompuZone.Application.Extentions;
using CompuZone.Application.Features.Dtos.Responses.CustomerResponses;
using CompuZone.Application.Wapper;
using CompuZone.Domain;
using CompuZone.Domain.Entities;
using CompuZone.Domain.Extentions;
using CompuZone.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CompUZone.Models; // Ensure this namespace matches your Customer entity location

namespace CompuZone.Application.Features.Queries.CustomerQueries
{
    public enum CustomerSortBy
    {
        FullName = 1,
        Email = 2,
        Username = 3,
        Phone = 4,
        Address = 5,
        DateOfBirth = 6,
    }

    public class GetCustomersQuery :
        PaginateBaseParamter,
        IRequest<Response<PaginatedList<CustomerReadReponseDto>>>
    {
        public string? TextSeach { get; set; }
        public CustomerSortBy? OrderBy { get; set; }
        public bool? IsArchived { get; set; }
        public bool IsDesc { get; set; }
    }

    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, Response<PaginatedList<CustomerReadReponseDto>>>
    {
        private readonly IGenericRepository<Customer> _repository;
        private readonly IMapper _mapper;

        public GetCustomersQueryHandler(IGenericRepository<Customer> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<PaginatedList<CustomerReadReponseDto>>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var query = _repository.GetAllAsync()
                              .IF(request.IsArchived != null, a => a.IArchived == request.IsArchived)
                              .Where(a =>
                                         // Updated search to match new Entity properties
                                         a.Full_name.ToLower().Contains(request.TextSeach.ToLower()) ||
                                         a.Email.ToLower().Contains(request.TextSeach.ToLower()) ||
                                         a.Username.ToLower().Contains(request.TextSeach.ToLower()) ||
                                         a.Phone.ToLower().Contains(request.TextSeach.ToLower()) ||
                                         a.Address.ToLower().Contains(request.TextSeach.ToLower())
                              )
                              .OrderGroupBy(new List<(bool condition, Expression<Func<Customer, object>>)>
                              {
                                 // Updated sort logic
                                 ( CustomerSortBy.FullName == request.OrderBy ,  a => a.Full_name),
                                 ( CustomerSortBy.Email == request.OrderBy ,     a => a.Email),
                                 ( CustomerSortBy.Username == request.OrderBy ,  a => a.Username),
                                 ( CustomerSortBy.Address == request.OrderBy ,   a => a.Address),
                                 ( CustomerSortBy.Phone == request.OrderBy ,     a => a.Phone),
                                 ( CustomerSortBy.DateOfBirth == request.OrderBy,a => a.DateOfBirth),
                              }, IsDesc: true)
                              .Select(a => new CustomerReadReponseDto
                              {
                                  // Assuming DTO is also updated to match these fields
                                  ID = a.ID, // Mapping CustomerID
                                  FullName = a.Full_name,
                                  Email = a.Email,
                                  Address = a.Address,
                                  DateOfBirth = a.DateOfBirth,
                                  Phone = a.Phone,
                                  Username = a.Username,
                                  IsArchived = a.IArchived
                              });

            var count = query.Count();
            var response = query.Paginate(request.PageNumber, request.PageSize);

            return new Response<PaginatedList<CustomerReadReponseDto>>
            (
                new PaginatedList<CustomerReadReponseDto>(response, count, request.PageNumber, request.PageSize)
            );
        }
    }
}