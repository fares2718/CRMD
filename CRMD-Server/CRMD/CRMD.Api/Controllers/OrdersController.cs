using System.Data;
using CRMD.Application.Orders.Commands;
using CRMD.Application.Orders.Queries;
using CRMD.Contracts.Employees.Post;
using CRMD.Contracts.Orders.Get;
using CRMD.Contracts.Orders.Post;
using ErrorOr;

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

    [HttpPost("add-order", Name = "add-order")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> AddOrder(AddOrderRequest request)
    {
        var cmd = new AddOrderCommand(request.OrderItemsDtos,
            (short)request.OrderType,
            request.TableId,
            request.CaptainId,
            request.TotalAmount,
            request.CreatedAt);
        var AddOrderResult = await _mediator.Send(cmd);
        return AddOrderResult.MatchFirst(
            created => CreatedAtRoute("place-order", new AddResponse(created)),
            error => error.Type == ErrorType.Validation ? BadRequest(new AddResponse(error)) : Problem(new AddResponse(error).ToString())
        );
    }

    [HttpGet("get-orders-by-date/{date}", Name = "get-orders-by-date")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> GetOrdersByDate(DateTime date)
    {
        var query = new GetOrdersByDateQuery(date);
        var getOrdersResult = await _mediator.Send(query);
        return getOrdersResult.MatchFirst(
            orders => Ok(new GetOrdersByDateResponse(orders)),
            error => error.Type == ErrorType.Validation ? BadRequest(new AddResponse(error)) : Problem(error.Description)
        );
    }


}