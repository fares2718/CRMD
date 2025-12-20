using CRMD.Application.Services;
using CRMD.Contracts.Orders;
using Microsoft.AspNetCore.Mvc;

namespace CRMD.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrdersService _ordersService;

    public OrdersController(IOrdersService ordersService)
    {
        _ordersService = ordersService;
    }
    [HttpPost]
    public IActionResult PlaceAnOrder(PlaceAnOrderRequest request)
    {
        var orderId = _ordersService.PlaceAnOrder(request.OrderItemsIds, (short)request.OrderType);
        var response = new PlaceAnOrderResponse(orderId);
        return Ok(response);
    }
}