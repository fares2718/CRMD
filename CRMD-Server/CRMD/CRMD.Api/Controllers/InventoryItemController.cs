using CRMD.Application.Generics.Commands;
using CRMD.Application.Generics.Queries;
using CRMD.Application.InventoryItems.Commands;
using CRMD.Contracts.InventoryItems.Post;
using CRMD.Contracts.InventoryItems.Put;
using CRMD.Domain.InventoryItems;
using ErrorOr;

namespace CRMD.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryItemController : ControllerBase
    {
        private readonly ISender _mediator;

        public InventoryItemController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add-InventoryItem", Name = "add-InventoryItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> AddInventoryItem(AddInventoryItemRequest request)
        {
            var cmd = new AddInventoryItemCommand(request.ItemId, request.Quantity, request.MinLevel);
            var addInventoryItemResult = await _mediator.Send(cmd);
            return addInventoryItemResult.MatchFirst(
                success => Ok(new sucAddResponse(success)),
                error => error.Type == ErrorType.Validation ? BadRequest(new AddResponse(error)) :
                Problem(new AddResponse(error).ToString())
            );
        }

        [HttpDelete("delete-InventoryItem/{id}", Name = "delete-InventoryItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var cmd = new DeleteCommand<InventoryItem>(id);
            var deleteInventoryItemResult = await _mediator.Send(cmd);
            return deleteInventoryItemResult.MatchFirst(
                deleted => Ok(new DeleteResponse(deleted)),
                error => error.Type == ErrorType.Validation ? BadRequest(new DeleteResponse(error))
                : Problem(new DeleteResponse(error).ToString())
            );
        }

        [HttpGet("get-inventory-items", Name = "get-inventory-items")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        public async Task<IActionResult> GetAllDepartment()
        {
            var query = new GetAllQuery<InventoryItem>();
            var getInventoryItemsResult = await _mediator.Send(query);
            return getInventoryItemsResult.MatchFirst(
                InventoryItems => Ok(new GetAllResponse<InventoryItem>(InventoryItems)),
                error => error.Type == ErrorType.NotFound ? NotFound(new GetAllResponse<InventoryItem>(error))
                : Problem(new GetAllResponse<InventoryItem>(error).ToString())
            );
        }

        [HttpGet("get-inventory-item/{id}", Name = "get-inventory-item")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetDepartmentById(int Id)
        {
            var query = new GetByIdQuery<InventoryItem>(Id);
            var getInventoryItemResult = await _mediator.Send(query);
            return getInventoryItemResult.MatchFirst(
                InventoryItem => Ok(new GetByIdResponse<InventoryItem>(InventoryItem)),
                error => error.Type == ErrorType.Validation ? BadRequest(new GetByIdResponse<InventoryItem>(error))
                : error.Type == ErrorType.NotFound ? NotFound(new GetByIdResponse<InventoryItem>(error))
                : Problem(new GetByIdResponse<InventoryItem>(error).ToString())
            );
        }

        [HttpPut("update-InventoryItem", Name = "update-InventoryItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> UpdateDepartment(UpdateInventoryItemRequest request)
        {
            var cmd = new UpdateInventoryItemCommand(request.ItemId, request.Quantity);
            var updateInventoryItemResult = await _mediator.Send(cmd);
            return updateInventoryItemResult.MatchFirst(
                updated => Ok(new UpdateResponse(updated)),
                error => error.Type == ErrorType.Validation ? BadRequest(new UpdateResponse(error))
                : Problem(new UpdateResponse(error).ToString())
            );
        }
    }
}