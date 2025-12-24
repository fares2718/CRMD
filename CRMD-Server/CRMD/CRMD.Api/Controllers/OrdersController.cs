using CRMD.Application.Orders.Commands;
using CRMD.Application.Orders.Queries;
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
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> PlaceAnOrder(PlaceAnOrderRequest request)
    {
        if(request.CaptainId < 1
           || request.TableId < 1 || request.OrderItemsDtos.Count <= 0)
            return BadRequest("Invalid request");
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

    [HttpGet(Name = "get-orders-by-date")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> GetOrdersByDate(GetOrdersByDateRequest request)
    {
        if (request.Date == default)
            return BadRequest("Invalid date");
        var query = new GetOrdersByDateQuery(request.Date);
        var getOrdersResult = await _mediator.Send(query);
        return getOrdersResult.MatchFirst(
            orders => Ok(new GetOrdersByDateResponse(orders)),
            error => Problem(error.Description)
        );
    }
}