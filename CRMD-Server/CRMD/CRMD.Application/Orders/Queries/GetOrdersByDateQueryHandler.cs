namespace CRMD.Application.Orders.Queries;

public class GetOrdersByDateQueryHandler : IRequestHandler<GetOrdersByDateQuery, ErrorOr<List<OrderDto>>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrdersByDateQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<List<OrderDto>>> Handle(GetOrdersByDateQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetOrdersByDateAsync(request.Date);
        var ordersDto = _mapper.Map<List<OrderDto>>(orders);
        return ordersDto;
    }
}