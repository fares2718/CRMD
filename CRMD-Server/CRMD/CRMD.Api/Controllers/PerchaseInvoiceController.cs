using CRMD.Application.PerchaseInvoices;
using CRMD.Contracts.PurchaseInvoices.Post;

namespace CRMD.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerchaseInvoiceController : ControllerBase
    {
        private readonly ISender _mediator;

        public PerchaseInvoiceController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add-invoice", Name = "add-invoice")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> AddPerchaseInvoice(AddPerchaseInvoiceRequest request)
        {
            if (request.supplierId < 1
               || request.totalAmount <= 0 || request.invoiceItems.Count <= 0)
                return BadRequest("Invalid request");
            var cmd = new AddPerchaseInvoiceCommand(
                request.supplierId,
                request.totalAmount,
                (short)request.paymentStatus,
                request.date,
                request.invoiceItems);
            var addInvoiceResult = await _mediator.Send(cmd);
            return addInvoiceResult.MatchFirst(
                created => CreatedAtRoute("add-invoice", created),
                error => Problem(error.Description)
            );
        }
    }
}