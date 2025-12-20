using MediatR;

namespace CRMD.Application.Orders.Commands;

public record PlaceAnOrderCommand(
    List<int> OrderItemsIds, 
    short OrderType) : IRequest<int>;