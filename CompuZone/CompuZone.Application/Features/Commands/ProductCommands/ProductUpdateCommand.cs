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
    public class ProductUpdateCommand : IRequest<bool>
    {
        public int ID { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }

        public double BuyPrice { get; set; }
        public double SalePrice { get; set; }
        public double Quantity { get; set; }
        public int MinQuantity { get; set; }

        public int? ProductID { get; set; }
    }
    public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand, bool>
    {
        private readonly IGenericRepository<ProductCatalog> _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;
        private readonly SharedLocalizationService _localizationService;

        public ProductUpdateCommandHandler(IGenericRepository<ProductCatalog> repository, IMapper mapper
            , ICurrentUserService currentUser
            , SharedLocalizationService localizationService)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUser = currentUser;
            _localizationService = localizationService;
        }
        public async Task<bool> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
        {
            var Product = await _repository.GetByIDAsync(request.ID);

            if (Product == null)
                throw new NotFoundException(
                      _localizationService.GetString(SharedLocalizationKeys.Exceptions_Not_Found, _currentUser.Language)
                  );

            var ProductEntity =  _mapper.Map(request, Product);
            _repository.UpdateAsync(ProductEntity);
            var status = await _repository.SaveChangesAsync();

            return status;
        }
    }
}
