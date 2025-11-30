using AutoMapper;
using MediatR;
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
    public class ProductAddCommand : IRequest<bool>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
        public double BuyPrice { get; set; }
        public double SalePrice { get; set; }
        public double Quantity { get; set; }
        public int MinQuantity { get; set; }

        public int? CategoryID { get; set; }
    }
    public class ProductAddCommandHandler : IRequestHandler<ProductAddCommand, bool>
    {
        private readonly IGenericRepository<ProductCatalog> _repository;
        private readonly IMapper _mapper;

        public ProductAddCommandHandler(IGenericRepository<ProductCatalog> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(ProductAddCommand request, CancellationToken cancellationToken)
        {
            var Product = _mapper.Map<ProductCatalog>(request);
            _repository.AddAsync(Product);
            var status = await _repository.SaveChangesAsync();

            return status;
        }
    }
}
