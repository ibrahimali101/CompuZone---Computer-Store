using AutoMapper;
using MediatR;
using CompuZone.Application.Exceptions;
using CompuZone.Application.Features.Dtos.Responses.CustomerResponses;
using CompuZone.Domain.Entities;
using CompuZone.Domain.Interfaces;
using System;
using System.Threading.Tasks;
using CompUZone.Models;

namespace CompuZone.Application.Features.Queries.CustomerQueries
{
    public class GetCustomerByIdQuery : IRequest<CustomerReadReponseDto>
    {
        public int Id { get; set; }
    }

    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerReadReponseDto>
    {
        private readonly IGenericRepository<Customer> _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public GetCustomerByIdQueryHandler(IGenericRepository<Customer> repository,
            IMapper mapper,
            ICurrentUserService currentUser)
        {
            _repository = repository;
            _mapper = mapper;

            _currentUser = currentUser;
        }

        public async Task<CustomerReadReponseDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _repository.GetByIDAsync(request.Id);

            if (customer == null)
                throw new NotFoundException($"Category with ID {request.Id} was not found.");

            return _mapper.Map<CustomerReadReponseDto>(customer);
        }
    }
}