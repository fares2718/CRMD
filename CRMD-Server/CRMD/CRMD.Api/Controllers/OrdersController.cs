using CRMD.Application.Orders.Commands;
using CRMD.Contracts.Orders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CRMD.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ISender _mediator;

    public OrdersController(ISender mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> PlaceAnOrder(PlaceAnOrderRequest request)
    {
        var cmd = new PlaceAnOrderCommand(request.OrderItemsIds,(short)request.OrderType);
        var placeAnOrderResult = await _mediator.Send(cmd);
        return placeAnOrderResult.MatchFirst(
            orderId => Ok(new PlaceAnOrderResponse(orderId)),
            error => Problem(error.Description)
        );
    }
}