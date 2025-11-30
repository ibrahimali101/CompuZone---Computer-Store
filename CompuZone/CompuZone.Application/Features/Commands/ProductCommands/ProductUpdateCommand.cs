using AutoMapper;
using MediatR;
using CompuZone.Application.Exceptions;
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
        public string Name { get; set; }
        public string Description { get; set; }

        public double Price { get; set; }
        public double Quantity { get; set; }

        public int? ProductID { get; set; }
    }
    public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand, bool>
    {
        private readonly IGenericRepository<Product> _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public ProductUpdateCommandHandler(IGenericRepository<Product> repository, IMapper mapper
            , ICurrentUserService currentUser)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUser = currentUser;
        }
        public async Task<bool> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
        {
            var Product = await _repository.GetByIDAsync(request.ID);

            if (Product == null)
                throw new NotFoundException($"Product with ID {request.ID} was not found.");

            var ProductEntity =  _mapper.Map(request, Product);
            _repository.UpdateAsync(ProductEntity);
            var status = await _repository.SaveChangesAsync();

            return status;
        }
    }
}
