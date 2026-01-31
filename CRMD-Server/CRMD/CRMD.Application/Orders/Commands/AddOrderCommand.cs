namespace CRMD.Application.Orders.Commands;

public record AddOrderCommand(
    List<OrderItemsDto> OrderItemsDtos,
    short OrderType,
    int TableId,
    int CaptainId,
    decimal TotalAmount,
    DateTime CreatedAt) : IRequest<ErrorOr<Created>>;