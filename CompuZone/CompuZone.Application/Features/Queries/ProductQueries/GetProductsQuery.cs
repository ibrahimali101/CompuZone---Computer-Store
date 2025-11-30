using AutoMapper;
using MediatR;
using CompuZone.Application.Extentions;
using CompuZone.Application.Features.Dtos.Responses.CategoryResponses;
using CompuZone.Application.Features.Dtos.Responses.ProductResponses;
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

namespace CompuZone.Application.Features.Queries.ProductQueries
{
    public enum ProductSortBy
    {
        NameAr = 1,
        NameEn = 2,
        DescriptionAr = 3,
        DescriptionEn = 4,
        BuyPrice = 5,
        SalePrice = 6,
        Quantity = 7,
        MinQuantity = 8
    }
    public class GetProductsQuery  : 
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

            var query = _repository.GetAllWithCategoryAsync()
                              .IF(request.IsArchived != null, a => a.IArchived == request.IsArchived)
                              .FilterText(request.TextSeach)
                              .OrderGroupBy(new List<(bool condition, Expression<Func<Product, object>>)>
                              {
                                 ( ProductSortBy.NameAr == request.OrderBy ,  a => a.NameAr),
                                 ( ProductSortBy.NameEn == request.OrderBy ,  a => a.NameEn),
                                 ( ProductSortBy.DescriptionAr == request.OrderBy ,  a => a.DescriptionAr),
                                 ( ProductSortBy.DescriptionEn == request.OrderBy ,  a => a.DescriptionEn),
                                 ( ProductSortBy.BuyPrice == request.OrderBy ,  a => a.BuyPrice),
                                 ( ProductSortBy.SalePrice == request.OrderBy ,  a => a.SalePrice),
                                 ( ProductSortBy.MinQuantity == request.OrderBy ,  a => a.MinQuantity),
                                 ( ProductSortBy.Quantity == request.OrderBy ,  a => a.Quantity),
                              }, IsDesc: true)
                              .Select(a => new ProductReadReponseDto
                              {
                                  ID = a.ID,
                                  NameAr = a.NameAr,
                                  NameEn = a.NameEn,
                                  DescriptionAr = a.DescriptionAr,
                                  DescriptionEn = a.DescriptionEn,
                                  IsArchived = a.IArchived,
                                  BuyPrice = a.BuyPrice,
                                  SalePrice = a.SalePrice,
                                  Quantity = a.Quantity,
                                  MinQuantity = a.MinQuantity,  

                                  Category = a.Category != null ? new CategoryReadReponseDto
                                  {
                                      ID = a.Category.ID,
                                      NameAr = a.Category.NameAr,
                                      NameEn = a.Category.NameEn,
                                      DescriptionAr = a.Category.DescriptionAr,
                                      DescriptionEn = a.Category.DescriptionEn, 
                                      IsArchived = a.Category.IArchived,
                                  } : null
                              });

            var count = query.Count();
            var response  = query.Paginate(request.PageNumber , request.PageSize);

            return new Response<PaginatedList<ProductReadReponseDto>>
            (
                new PaginatedList<ProductReadReponseDto>(response, count, request.PageNumber, request.PageSize)
            );
        }
    }
}
