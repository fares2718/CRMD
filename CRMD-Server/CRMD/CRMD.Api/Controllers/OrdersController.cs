using CRMD.Contracts.Orders;
using Microsoft.AspNetCore.Mvc;

namespace CRMD.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    public IActionResult PlaceAnOrder(PlaceAnOrderRequest placeAnOrderRequest)
    {
        return Ok((placeAnOrderRequest));
    }
}