using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using CompuZone.Application.Extentions;
using CompuZone.Application.Features.Dtos.Responses.CategoryResponses;
using CompuZone.Application.Features.Dtos.Responses.ProductResponses;
using CompuZone.Application.Wapper;
using CompuZone.Domain;
using CompuZone.Domain.Extentions;
using CompuZone.Domain.Interfaces;
using CompUZone.Models;
using MediatR;

namespace CompuZone.Application.Features.Queries.ProductQueries
{
    public enum ProductSortBy
    {
        Name = 1,
        Description = 2,
        Price = 3,
        Quantity = 4
    }

    public class GetProductsQuery :
        PaginateBaseParamter,
        IRequest<Response<PaginatedList<ProductReadReponseDto>>>
    {
        public string? TextSeach { get; set; }
        public ProductSortBy? OrderBy { get; set; }
        public bool? IsArchived { get; set; }
        public bool IsDesc { get; set; }
    }

    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Response<PaginatedList<ProductReadReponseDto>>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<PaginatedList<ProductReadReponseDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            // Note: Ensure _repository.GetAllWithCategoryAsync() returns IQueryable<Product>
            var query = _repository.GetAllWithCategoryAsync()
                  .IF(request.IsArchived != null, a => a.IArchived == request.IsArchived)
                  // Updated Search Logic
                  .Where(a =>
                      a.Name.ToLower().Contains(request.TextSeach.ToLower()) ||
                      (a.Description != null && a.Description.ToLower().Contains(request.TextSeach.ToLower()))
                  )
                  // Updated Sort Logic
                  .OrderGroupBy(new List<(bool condition, Expression<Func<Product, object>>)>
                  {
                     ( ProductSortBy.Name == request.OrderBy,      a => a.Name),
                     ( ProductSortBy.Description == request.OrderBy,a => a.Description),
                     ( ProductSortBy.Price == request.OrderBy,      a => a.Price),
                     ( ProductSortBy.Quantity == request.OrderBy,   a => a.QuantityInStock),
                  }, IsDesc: true)
                  // Updated Projection
                  .Select(a => new ProductReadReponseDto
                  {
                      ID = a.ID,
                      Name = a.Name,
                      Description = a.Description,
                      Price = a.Price,
                      Quantity = a.QuantityInStock,
                      IsArchived = a.IArchived,

                      // Mapping the related Category (Assuming CategoryDto is already updated)
                      Category = a.Category != null ? new CategoryReadReponseDto
                      {
                          ID = a.Category.ID,
                          Name = a.Category.CategoryName,
                          IsArchived = a.Category.IArchived
                      } : null
                  });

            var count = query.Count();
            var response = query.Paginate(request.PageNumber, request.PageSize);

            return new Response<PaginatedList<ProductReadReponseDto>>
            (
                new PaginatedList<ProductReadReponseDto>(response, count, request.PageNumber, request.PageSize)
            );
        }
    }
}