namespace CRMD.Application.Orders.Queries;

public record GetOrdersByDateQuery(
    DateTime Date) : IRequest<ErrorOr<List<OrderDto>>>;