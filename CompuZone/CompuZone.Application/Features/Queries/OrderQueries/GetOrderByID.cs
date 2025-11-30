using AutoMapper;
using CompuZone.Application.Features.Dtos.Responses.OrderResponses;
using CompuZone.Application.Wapper;
using CompuZone.Domain.Interfaces;
using MediatR;

public class GetOrderByIdQuery : IRequest<Response<OrderItemReadResponseDTO>>
{
    public int Id { get; set; }
}

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Response<OrderItemReadResponseDTO>>
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;

    public GetOrderByIdQueryHandler(IOrderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Response<OrderItemReadResponseDTO>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetOrderWithItemsAsync(request.Id);

        if (order == null)
            return new Response<OrderItemReadResponseDTO>("Order not found");

        var dto = _mapper.Map<OrderItemReadResponseDTO>(order);
        return new Response<OrderItemReadResponseDTO>(dto);
    }
}