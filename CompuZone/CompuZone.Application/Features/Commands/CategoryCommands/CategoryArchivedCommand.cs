using AutoMapper;
using MediatR;
using CompuZone.Application.Exceptions;
using CompuZone.Application.Localization;
using CompuZone.Domain.Entities;
using CompuZone.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompUZone.Models;

namespace CompuZone.Application.Features.Commands.CategoryCommands
{
    public class CategoryArchivedCommand : IRequest<bool>
    {
        public int ID { get; set; }
    }
    public class CategoryArchivedCommandHandler : IRequestHandler<CategoryArchivedCommand, bool>
    {
        private readonly IGenericRepository<Category> _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;
        private readonly SharedLocalizationService _localizationService;

        public CategoryArchivedCommandHandler(IGenericRepository<Category> repository, IMapper mapper
            , ICurrentUserService currentUser
            , SharedLocalizationService localizationService)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUser = currentUser;
            _localizationService = localizationService;
        }
        public async Task<bool> Handle(CategoryArchivedCommand request, CancellationToken cancellationToken)
        {
            var category =  await _repository.GetByIDAsync(request.ID);

            if (category == null)
                throw new NotFoundException(
                        _localizationService.GetString(SharedLocalizationKeys.Exceptions_Not_Found, _currentUser.Language)
                    );

            _repository.ArchivedAsync(category);
            var status = await _repository.SaveChangesAsync();

            return status;
        }
    }

}
