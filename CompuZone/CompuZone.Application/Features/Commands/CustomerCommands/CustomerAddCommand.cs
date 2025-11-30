using AutoMapper;
using CompUZone.Models;
using MediatR;
using CompuZone.Domain.Entities;
using CompuZone.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.Domain.Interfaces;

namespace CompuZone.Application.Features.Commands.CustomerCommands
{
    public class CustomerAddCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }

    }
    public class CustomerAddCommandHandler : IRequestHandler<CustomerAddCommand, bool>
    {
        private readonly IGenericRepository<Customer> _repository;
        private readonly IMapper _mapper;

        public CustomerAddCommandHandler(IGenericRepository<Customer> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(CustomerAddCommand request, CancellationToken cancellationToken)
        {
            var Customer = _mapper.Map<Customer>(request);
             _repository.AddAsync(Customer);
            var status = await _repository.SaveChangesAsync();

            return status;
        }
    }
}
