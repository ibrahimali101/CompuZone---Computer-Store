using AutoMapper;
using MediatR;
using CompuZone.Application.Extentions;
using CompuZone.Application.Features.Dtos.Responses.CategoryResponses;
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

namespace CompuZone.Application.Features.Queries
{
    public enum CategorySortBy
    {
        NameAr = 1,
        NameEn = 2,
        DescriptionAr = 3,
        DescriptionEn = 4
    }
    public class GetCategoriesQuery  : 
        PaginateBaseParamter,
        IRequest<Response<PaginatedList<CategoryReadReponseDto>>>
    {

        public string? TextSeach { get; set; }
        public CategorySortBy? OrderBy { get; set; }
        public bool? IsArchived { get; set; }
        public bool IsDesc { get; set; }
    }
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, Response<PaginatedList<CategoryReadReponseDto>>>
    {
        private readonly IGenericRepository<ProductCategory> _repository;
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(IGenericRepository<ProductCategory> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Response<PaginatedList<CategoryReadReponseDto>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {

            var query = _repository.GetAllAsync()
                              .IF(request.IsArchived != null, a => a.IArchived == request.IsArchived)
                              .FilterText(request.TextSeach)
                              .OrderGroupBy(new List<(bool condition, Expression<Func<ProductCategory, object>>)>
                              {
                                 ( CategorySortBy.NameAr == request.OrderBy ,  a => a.NameAr),
                                 ( CategorySortBy.NameEn == request.OrderBy ,  a => a.NameEn),
                                 ( CategorySortBy.DescriptionAr == request.OrderBy ,  a => a.DescriptionAr),
                                 ( CategorySortBy.DescriptionEn == request.OrderBy ,  a => a.DescriptionEn),
                              }, IsDesc: true)
                              .Select(a => new CategoryReadReponseDto
                              {
                                  ID = a.ID,
                                  NameAr = a.NameAr,
                                  NameEn = a.NameEn,
                                  DescriptionAr = a.DescriptionAr,
                                  DescriptionEn = a.DescriptionEn,
                                  IsArchived = a.IArchived
                              });

            var count = query.Count();
            var response  = query.Paginate(request.PageNumber , request.PageSize);

            return new Response<PaginatedList<CategoryReadReponseDto>>
            (
                new PaginatedList<CategoryReadReponseDto>(response, count, request.PageNumber, request.PageSize)
            );
        }
    }
}
