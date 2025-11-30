using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CompuZone.Application.Exceptions;
using CompuZone.Application.Localization;
using CompuZone.Domain.Entities;
using CompuZone.Domain.Interfaces;
using CompUZone.Models;
using MediatR;

namespace CompuZone.Application.Features.Commands.CategoryCommands
{
    public class CategoryUnArchivedCommand : IRequest<bool>
    {
        public int ID { get; set; }
    }
    public class CategoryUnArchivedCommandHandler : IRequestHandler<CategoryUnArchivedCommand, bool>
    {
        private readonly IGenericRepository<ProductCategory> _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;
        private readonly SharedLocalizationService _localizationService;

        public CategoryUnArchivedCommandHandler(IGenericRepository<ProductCategory> repository, IMapper mapper
            , ICurrentUserService currentUser
            , SharedLocalizationService localizationService)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUser = currentUser;
            _localizationService = localizationService;
        }
        public async Task<bool> Handle(CategoryUnArchivedCommand request, CancellationToken cancellationToken)
        {
            var category =  await _repository.GetByIDAsync(request.ID);

            if (category == null)
                throw new NotFoundException(
                      _localizationService.GetString(SharedLocalizationKeys.Exceptions_Not_Found, _currentUser.Language)
                  );

            _repository.UnArchivedAsync(category);
            var status = await _repository.SaveChangesAsync();

            return status;
        }
    }
}
