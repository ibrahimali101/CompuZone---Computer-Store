using AutoMapper;
using MediatR;
using CompuZone.Application.Exceptions;
using CompuZone.Application.Features.Dtos.Responses.CategoryResponses;
using CompuZone.Application.Localization;
using CompuZone.Domain.Entities;
using CompuZone.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompUZone.Models;

namespace CompuZone.Application.Features.Queries
{
    public class GetCategoryByIdQuery : IRequest<CategoryReadReponseDto>
    {
        public int Id { get; set; }
    }
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryReadReponseDto>
    {
        private readonly IGenericRepository<Category> _repository;
        private readonly IMapper _mapper;
        private readonly SharedLocalizationService _localizationService;
        private readonly ICurrentUserService _currentUser;

        public GetCategoryByIdQueryHandler(IGenericRepository<Category> repository ,
            IMapper mapper , 
            SharedLocalizationService localizationService,
            ICurrentUserService currentUser)
        {
            _repository = repository;
            _mapper = mapper;
            _localizationService = localizationService;
            _currentUser = currentUser;
        }
        public async Task<CategoryReadReponseDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIDAsync(request.Id);

            if (category == null) 
                throw new NotFoundException(
                    _localizationService.GetString(SharedLocalizationKeys.Exceptions_Not_Found , _currentUser.Language)
                );

            return _mapper.Map<CategoryReadReponseDto>(category);
        }
    }
}
