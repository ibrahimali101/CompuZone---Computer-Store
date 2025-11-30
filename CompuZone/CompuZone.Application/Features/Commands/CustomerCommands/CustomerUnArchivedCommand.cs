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

namespace CompuZone.Application.Features.Commands.CustomerCommands
{
    public class CustomerUnArchivedCommand : IRequest<bool>
    {
        public int ID { get; set; }
    }
    public class CustomerUnArchivedCommandHandler : IRequestHandler<CustomerUnArchivedCommand, bool>
    {
        private readonly IGenericRepository<Customer> _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public CustomerUnArchivedCommandHandler(IGenericRepository<Customer> repository, IMapper mapper
            , ICurrentUserService currentUser)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUser = currentUser;
        }
        public async Task<bool> Handle(CustomerUnArchivedCommand request, CancellationToken cancellationToken)
        {
            var Customer =  await _repository.GetByIDAsync(request.ID);

            if (Customer == null)
                throw new NotFoundException($"Customer with ID {request.ID} was not found.");

            _repository.UnArchivedAsync(Customer);
            var status = await _repository.SaveChangesAsync();

            return status;
        }
    }
}
