namespace CRMD.Application.Orders.Commands;

public record PlaceAnOrderCommand(
    List<OrderItemsDto> OrderItemsDtos,
    short OrderType,
    int TableId,
    int CaptainId,
    decimal TotalAmount,
    DateTime CreatedAt) : IRequest<ErrorOr<Created>>;