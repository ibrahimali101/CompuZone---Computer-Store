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

        public OrderUpdateCommandHandler(IGenericRepository<Order> repository, IMapper mapper
            , ICurrentUserService currentUser)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUser = currentUser;
        }
        public async Task<bool> Handle(OrderUpdateCommand request, CancellationToken cancellationToken)
        {
            var Order = await _repository.GetByIDAsync(request.ID);

            if (Order == null)
                throw new NotFoundException($"Order with ID {request.ID} was not found.");

            var OrderEntity =  _mapper.Map(request, Order);
            _repository.UpdateAsync(OrderEntity);
            var status = await _repository.SaveChangesAsync();
            return status;
        }
    }
}
