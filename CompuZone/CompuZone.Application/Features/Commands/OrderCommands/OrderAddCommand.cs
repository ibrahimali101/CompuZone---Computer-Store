using AutoMapper;
using MediatR;
using CompuZone.Application.Features.Dtos.Requests;
using CompuZone.Domain.Entities;
using CompuZone.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompuZone.Domain.Interfaces;

namespace CompuZone.Application.Features.Commands
{
    public class OrderAddCommand : IRequest<bool>
    {
        public DateTime DateOrder { get; set; }
        public int TotalOrder { get; set; }
        public int CustomerID { get; set; }

        public IEnumerable<OrderProductRequestDto> OrderProducts { get; set; }
    }
    public class OrderAddCommandHandler : IRequestHandler<OrderAddCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUser;
        private readonly IMapper _mapper;

        public OrderAddCommandHandler(
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUser,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _mapper = mapper;
        }
        public async Task<bool> Handle(OrderAddCommand request, CancellationToken cancellationToken)
        {
            var StockId = _currentUser.StockId == 0 ? 1 : _currentUser.StockId;

            var status = await _unitOfWork.SaveChangesAsync();
            return status;
        }
    }
}
