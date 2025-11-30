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
    public class CustomerUpdateCommand : IRequest<bool>
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
    }
    public class CustomerUpdateCommandHandler : IRequestHandler<CustomerUpdateCommand, bool>
    {
        private readonly IGenericRepository<Customer> _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public CustomerUpdateCommandHandler(IGenericRepository<Customer> repository, IMapper mapper
            , ICurrentUserService currentUser)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUser = currentUser;
        }
        public async Task<bool> Handle(CustomerUpdateCommand request, CancellationToken cancellationToken)
        {
            var Customer = await _repository.GetByIDAsync(request.ID);

            if (Customer == null)
                throw new NotFoundException($"Customer with ID {request.ID} was not found.");

            var CustomerEntity =  _mapper.Map(request, Customer);
            _repository.UpdateAsync(CustomerEntity);
            var status = await _repository.SaveChangesAsync();

            return status;
        }
    }
}
