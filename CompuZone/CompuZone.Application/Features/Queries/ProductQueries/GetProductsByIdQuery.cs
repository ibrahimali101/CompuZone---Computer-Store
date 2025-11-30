using AutoMapper;
using MediatR;
using CompuZone.Application.Exceptions;
using CompuZone.Application.Features.Dtos.Responses.ProductResponses;
using CompuZone.Application.Localization;
using CompuZone.Domain.Entities;
using CompuZone.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private readonly SharedLocalizationService _localizationService;
        private readonly ICurrentUserService _currentUser;

        public GetProductByIdQueryHandler(IProductRepository repository ,
            IMapper mapper , 
            SharedLocalizationService localizationService,
            ICurrentUserService currentUser)
        {
            _repository = repository;
            _mapper = mapper;
            _localizationService = localizationService;
            _currentUser = currentUser;
        }
        public async Task<ProductReadReponseDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var Product = await _repository.GetByIDWithCategoryAsync(request.Id);

            if (Product == null) 
                throw new NotFoundException(
                    _localizationService.GetString(SharedLocalizationKeys.Exceptions_Not_Found , _currentUser.Language)
                );

            return _mapper.Map<ProductReadReponseDto>(Product);

            //return new ProductReadReponseDto
            //{
            //     ID = Product.ID,
            //     NameAr = Product.NameAr,
            //     NameEn = Product.NameEn,
            //     DescriptionAr = Product.DescriptionAr,
            //     DescriptionEn = Product.DescriptionEn,    
            //     IsArchived = Product.IArchived
            //};
        }
    }
}
