using MediatR;
using ErrorOr;
namespace CRMD.Application.Orders.Commands;

public record PlaceAnOrderCommand(
    List<int> OrderItemsIds, 
    short OrderType) : IRequest<ErrorOr<int>>;