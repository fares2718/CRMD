namespace CRMD.Application.Orders.Commands;

public class PlaceAnOrderCommandHandler : IRequestHandler<PlaceAnOrderCommand, ErrorOr<Created>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    public PlaceAnOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<Created>> Handle(PlaceAnOrderCommand request, CancellationToken cancellationToken)
    {
        if (request.TableId < 0 || request.CaptainId < 0
                                || request.OrderItemsDtos.Count == 0 || request.OrderType < 0)
            return Error.Validation();
        var order = _mapper.Map<Order>(request);
        await _orderRepository.AddOrderAsync(order);
        return Result.Created;
    }
}