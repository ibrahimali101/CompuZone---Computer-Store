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

namespace CompuZone.Application.Features.Commands.ProductCommands
{
    public class ProductArchivedCommand : IRequest<bool>
    {
        public int ID { get; set; }
    }
    public class ProductArchivedCommandHandler : IRequestHandler<ProductArchivedCommand, bool>
    {
        private readonly IGenericRepository<Product> _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;
        private readonly SharedLocalizationService _localizationService;

        public ProductArchivedCommandHandler(IGenericRepository<Product> repository, IMapper mapper
            , ICurrentUserService currentUser
            , SharedLocalizationService localizationService)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUser = currentUser;
            _localizationService = localizationService;
        }
        public async Task<bool> Handle(ProductArchivedCommand request, CancellationToken cancellationToken)
        {
            var Product =  await _repository.GetByIDAsync(request.ID);

            if (Product == null)
                throw new NotFoundException(
                       _localizationService.GetString(SharedLocalizationKeys.Exceptions_Not_Found, _currentUser.Language)
                   );

            _repository.ArchivedAsync(Product);
            var status = await _repository.SaveChangesAsync();

            return status;
        }
    }

}
