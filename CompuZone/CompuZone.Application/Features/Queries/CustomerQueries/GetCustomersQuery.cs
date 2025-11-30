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
using System.Text;
using System.Threading.Tasks;
using CompUZone.Models;

namespace CompuZone.Application.Features.Queries.CustomerQueries
{
    public enum CustomerSortBy
    {
        NameAr = 1,
        NameEn = 2,
        PhoneNumber = 3,
        Address = 4,
        BirthDate = 5,
    }
    public class GetCustomersQuery  : 
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
                                         a.NameAr.ToLower().Contains(request.TextSeach.ToLower()) ||
                                         a.NameEn.ToLower().Contains(request.TextSeach.ToLower()) ||
                                         a.PhoneNumber.ToLower().Contains(request.TextSeach.ToLower()) ||
                                         a.Address.ToLower().Contains(request.TextSeach.ToLower()) 
                              )
                              .OrderGroupBy(new List<(bool condition, Expression<Func<Customer, object>>)>
                              {
                                 ( CustomerSortBy.NameAr == request.OrderBy ,  a => a.NameAr),
                                 ( CustomerSortBy.NameEn == request.OrderBy ,  a => a.NameEn),
                                 ( CustomerSortBy.Address == request.OrderBy ,  a => a.Address),
                                 ( CustomerSortBy.PhoneNumber == request.OrderBy ,  a => a.PhoneNumber),
                                 ( CustomerSortBy.BirthDate == request.OrderBy ,  a => a.BirthDate),
                              }, IsDesc: true)
                              .Select(a => new CustomerReadReponseDto
                              {
                                  ID = a.ID,
                                  NameAr = a.NameAr,
                                  NameEn = a.NameEn,
                                  Address = a.Address,
                                  BirthDate = a.BirthDate,
                                  PhoneNumber = a.PhoneNumber,
                                  IsArchived = a.IArchived
                              });

            var count = query.Count();
            var response  = query.Paginate(request.PageNumber , request.PageSize);

            return new Response<PaginatedList<CustomerReadReponseDto>>
            (
                new PaginatedList<CustomerReadReponseDto>(response, count, request.PageNumber, request.PageSize)
            );
        }
    }
}
