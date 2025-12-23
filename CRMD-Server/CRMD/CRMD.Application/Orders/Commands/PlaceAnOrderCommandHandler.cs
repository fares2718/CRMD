using CRMD.Application.Common.Interfaces;
using CRMD.Domain.Orders;
using MediatR;
using ErrorOr;

namespace CRMD.Application.Orders.Commands;

public class PlaceAnOrderCommandHandler : IRequestHandler<PlaceAnOrderCommand,ErrorOr<int>>
{
    private readonly IOrderRepository _orderRepository;
    public PlaceAnOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<ErrorOr<int>> Handle(PlaceAnOrderCommand request, CancellationToken cancellationToken)
    {
        //Creat Order
        var order = new Order
        {
            //Map the Order
        };
        //Send It to the database
        await _orderRepository.AddOrderAsync(order);
        //return Error or Id;
        return 1;
    }
}