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

namespace CompuZone.Application.Features.Commands.OrderCommands
{
    public class OrderUpdateCommand : IRequest<bool>
    {
        public int ID { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionAr { get; set; }
        public string DescriptionEn { get; set; }
    }
    public class OrderUpdateCommandHandler : IRequestHandler<OrderUpdateCommand, bool>
    {
        private readonly IGenericRepository<Order> _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;
        private readonly SharedLocalizationService _localizationService;

        public OrderUpdateCommandHandler(IGenericRepository<Order> repository, IMapper mapper
            , ICurrentUserService currentUser
            , SharedLocalizationService localizationService)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUser = currentUser;
            _localizationService = localizationService;
        }
        public async Task<bool> Handle(OrderUpdateCommand request, CancellationToken cancellationToken)
        {
            var Order = await _repository.GetByIDAsync(request.ID);

            if (Order == null)
                throw new NotFoundException(
                      _localizationService.GetString(SharedLocalizationKeys.Exceptions_Not_Found, _currentUser.Language)
                  );

            var OrderEntity =  _mapper.Map(request, Order);
            _repository.UpdateAsync(OrderEntity);
            var status = await _repository.SaveChangesAsync();
            return status;
        }
    }
}
