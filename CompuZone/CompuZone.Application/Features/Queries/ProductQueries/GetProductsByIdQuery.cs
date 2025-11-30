using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CompuZone.Application.Exceptions;
using CompuZone.Application.Features.Dtos.Responses.ProductResponses;
using CompuZone.Domain.Entities;
using CompuZone.Domain.Interfaces;
using CompUZone.Models;
using MediatR;

namespace CompuZone.Application.Features.Queries.ProductQueries
{
    public class GetProductByIdQuery : IRequest<ProductReadReponseDto>
    {
        public int Id { get; set; }
    }
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductReadReponseDto>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public GetProductByIdQueryHandler(IProductRepository repository ,
            IMapper mapper , 
            ICurrentUserService currentUser)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUser = currentUser;
        }
        public async Task<ProductReadReponseDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var Product = await _repository.GetByIDWithCategoryAsync(request.Id);

            if (Product == null)
                throw new NotFoundException($"Category with ID {request.Id} was not found.");
            return _mapper.Map<ProductReadReponseDto>(Product);
        }
    }
}
