using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using CompuZone.Application.Extentions;
using CompuZone.Application.Features.Dtos.Responses.CategoryResponses;
using CompuZone.Application.Wapper;
using CompuZone.Domain;
using CompuZone.Domain.Extentions;
using CompuZone.Domain.Interfaces; // Updated to use the Interface if needed, or keep Generic
using CompUZone.Models;
using MediatR;

namespace CompuZone.Application.Features.Queries
{
    public enum CategorySortBy
    {
        Name = 1,
        Id = 2
    }

    public class GetCategoriesQuery :
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
        private readonly IGenericRepository<Category> _repository;
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(IGenericRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<PaginatedList<CategoryReadReponseDto>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var query = _repository.GetAllAsync()
                  .IF(request.IsArchived != null, a => a.IArchived == request.IsArchived)
                  // Updated Search to only look at CategoryName
                  .Where(a => a.CategoryName.ToLower().Contains(request.TextSeach.ToLower()))
                  .OrderGroupBy(new List<(bool condition, Expression<Func<Category, object>>)>
                  {
                     // Updated Sort to match new Entity
                     ( CategorySortBy.Name == request.OrderBy ,  a => a.CategoryName),
                     ( CategorySortBy.Id == request.OrderBy ,    a => a.ID),
                  }, IsDesc: true)
                  .Select(a => new CategoryReadReponseDto
                  {
                      // IMPORTANT: I am assuming your DTO has also been updated to have just 'ID' and 'Name'
                      ID = a.ID,
                      Name = a.CategoryName,
                      IsArchived = a.IArchived
                  });

            var count = query.Count();
            var response = query.Paginate(request.PageNumber, request.PageSize);

            return new Response<PaginatedList<CategoryReadReponseDto>>
            (
                new PaginatedList<CategoryReadReponseDto>(response, count, request.PageNumber, request.PageSize)
            );
        }
    }
}