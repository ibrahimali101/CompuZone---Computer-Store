using AutoMapper;
using MediatR;
using CompuZone.Application.Exceptions;
using CompuZone.Application.Features.Dtos.Responses.CustomerResponses;
using CompuZone.Application.Localization;
using CompuZone.Domain.Entities;
using CompuZone.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private readonly SharedLocalizationService _localizationService;
        private readonly ICurrentUserService _currentUser;

        public GetCustomerByIdQueryHandler(IGenericRepository<Customer> repository ,
            IMapper mapper , 
            SharedLocalizationService localizationService,
            ICurrentUserService currentUser)
        {
            _repository = repository;
            _mapper = mapper;
            _localizationService = localizationService;
            _currentUser = currentUser;
        }
        public async Task<CustomerReadReponseDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var Customer = await _repository.GetByIDAsync(request.Id);

            if (Customer == null) 
                throw new NotFoundException(
                    _localizationService.GetString(SharedLocalizationKeys.Exceptions_Not_Found , _currentUser.Language)
                );

            return _mapper.Map<CustomerReadReponseDto>(Customer);

            //return new CustomerReadReponseDto
            //{
            //     ID = Customer.ID,
            //     NameAr = Customer.NameAr,
            //     NameEn = Customer.NameEn,
            //     DescriptionAr = Customer.DescriptionAr,
            //     DescriptionEn = Customer.DescriptionEn,    
            //     IsArchived = Customer.IArchived
            //};
        }
    }
}
