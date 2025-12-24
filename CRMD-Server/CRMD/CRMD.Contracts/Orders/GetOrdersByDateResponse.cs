using CRMD.Application.DTOs;

namespace CRMD.Contracts.Orders;

public record GetOrdersByDateResponse(List<OrderDto> Orders);