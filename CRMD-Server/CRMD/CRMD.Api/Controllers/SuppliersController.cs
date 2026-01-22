using CRMD.Application.Suppliers.Commands;
using CRMD.Application.Suppliers.Queries;
using CRMD.Contracts.Suppliers.Delete;
using CRMD.Contracts.Suppliers.Get;
using CRMD.Contracts.Suppliers.Post;
using ErrorOr;

namespace CRMD.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : ControllerBase
    {

        private readonly ISender _mediator;

        public SuppliersController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add-supplier", Name = "add-supplier")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> AddSupplier(AddSupplierRequest request)
        {
            if (string.IsNullOrEmpty(request.name) || string.IsNullOrEmpty(request.address) ||
            request.phones.Length == 0)
                return BadRequest("Invalid data");

            var cmd = new AddSupplierCommand
            (
                request.name,
                request.phones,
                request.address
            );
            var addSupplierResult = await _mediator.Send(cmd);

            return addSupplierResult.MatchFirst(
                created => CreatedAtRoute("add-supplier", new AddSupplierResponse(created)),
                error => error.Type == ErrorType.Validation ? (ObjectResult)BadRequest(new DeleteSupplierResponse(error))
                                                            : Problem(new DeleteSupplierResponse(error).ToString())
            );
        }

        [HttpDelete("delete-supplier/{supplierId}", Name = "delete-supplier")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> DeleteSupplier(int supplierId)
        {
            var cmd = new DeleteSupplierCommand(supplierId);

            var deleteSupplierResult = await _mediator.Send(cmd);
            return deleteSupplierResult.MatchFirst(
                deleted => Ok(new DeleteSupplierResponse(deleted)),
                error => error.Type == ErrorType.Validation ? (ObjectResult)BadRequest(new DeleteSupplierResponse(error))
                                                            : Problem(new DeleteSupplierResponse(error).ToString())
            );
        }


        [HttpGet("get-supplier/{id}", Name = "get-suppliers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetSupplierById(int id)
        {
            var query = new GetSupplierByIdQuery(id);
            var getSupplierResult = await _mediator.Send(query);

            return getSupplierResult.MatchFirst(
                supplier => Ok(new GetSupplierByIdResponse(supplier)),
                error => error.Type == ErrorType.Failure ? Problem(new GetSupplierByIdResponse(error).ToString())
                                : (ObjectResult)NotFound(new GetSupplierByIdResponse(error))
            );
        }


        [HttpGet("all-suppliers", Name = "all-suppliers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetSuppliers()
        {
            var query = new GetSuppliersQuery();
            var getSuppliersResult = await _mediator.Send(query);
            return getSuppliersResult.MatchFirst(
                suppliers => Ok(new GetSuppliersResponse(suppliers)),
                error => (ObjectResult)NotFound(new GetSuppliersResponse(error))
            );
        }

    }
}