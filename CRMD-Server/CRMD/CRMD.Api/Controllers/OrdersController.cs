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
    [HttpPost(Name = "place-order")]
    public async Task<IActionResult> PlaceAnOrder(PlaceAnOrderRequest request)
    {
        var cmd = new PlaceAnOrderCommand(request.OrderItemsDtos,
            (short)request.OrderType,
            request.TableId,
            request.CaptainId,
            request.TotalAmount,
            request.CreatedAt);
        var placeAnOrderResult = await _mediator.Send(cmd);
        return placeAnOrderResult.MatchFirst(
            created => CreatedAtRoute("place-order",new {request.CaptainId}),
            error => Problem(error.Description)
        );
    }
}