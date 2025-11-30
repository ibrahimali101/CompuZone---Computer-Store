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
    public class ProductArchivedCommand : IRequest<bool>
    {
        public int ID { get; set; }
    }
    public class ProductArchivedCommandHandler : IRequestHandler<ProductArchivedCommand, bool>
    {
        private readonly IGenericRepository<Product> _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public ProductArchivedCommandHandler(IGenericRepository<Product> repository, IMapper mapper
            , ICurrentUserService currentUser)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUser = currentUser;
        }
        public async Task<bool> Handle(ProductArchivedCommand request, CancellationToken cancellationToken)
        {
            var Product =  await _repository.GetByIDAsync(request.ID);

            if (Product == null)
                throw new NotFoundException($"Product with ID {request.ID} was not found.");

            _repository.ArchivedAsync(Product);
            var status = await _repository.SaveChangesAsync();

            return status;
        }
    }

}
