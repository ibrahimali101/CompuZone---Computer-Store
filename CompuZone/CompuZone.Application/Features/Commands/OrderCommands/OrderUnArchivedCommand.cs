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
    public class OrderUnArchivedCommand : IRequest<bool>
    {
        public int ID { get; set; }
    }
    public class OrderUnArchivedCommandHandler : IRequestHandler<OrderUnArchivedCommand, bool>
    {
        private readonly IGenericRepository<Order> _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;
        private readonly SharedLocalizationService _localizationService;

        public OrderUnArchivedCommandHandler(IGenericRepository<Order> repository, IMapper mapper
            , ICurrentUserService currentUser
            , SharedLocalizationService localizationService)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUser = currentUser;
            _localizationService = localizationService;
        }
        public async Task<bool> Handle(OrderUnArchivedCommand request, CancellationToken cancellationToken)
        {
            var Order =  await _repository.GetByIDAsync(request.ID);

            if (Order == null)
                throw new NotFoundException(
                      _localizationService.GetString(SharedLocalizationKeys.Exceptions_Not_Found, _currentUser.Language)
                  );

            _repository.UnArchivedAsync(Order);
            var status = await _repository.SaveChangesAsync();

            return status;
        }
    }
}
