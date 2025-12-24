using CRMD.Application.DTOs;
using ErrorOr;
using MediatR;

namespace CRMD.Application.Orders.Queries;

public record GetOrdersByDateQuery(
    DateTime Date) : IRequest<ErrorOr<List<OrderDto>>>;