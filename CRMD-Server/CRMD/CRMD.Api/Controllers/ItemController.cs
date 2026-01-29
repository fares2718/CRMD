using CRMD.Application.DTOs;
using CRMD.Application.Items.Commands;
using CRMD.Application.Items.Queries;
using CRMD.Contracts.Items.Post;
using CRMD.Contracts.Items.Put;
using ErrorOr;

namespace CRMD.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ISender _mediator;

        public ItemController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add-item", Name = "add-item")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> AddItem(AddItemRequest request)
        {
            var cmd = new AddItemCommand(request.CategoryId, request.Price, request.Name);
            var addItemResult = await _mediator.Send(cmd);
            return addItemResult.MatchFirst(
                created => CreatedAtRoute("add-item", new AddResponse(created)),
                error => error.Type == ErrorType.Validation ? BadRequest(new AddResponse(error)) :
                Problem(new AddResponse(error).ToString())
            );
        }

        [HttpDelete("delete-item/{id}", Name = "delete-item")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var cmd = new DeleteItemCommand(id);
            var deleteItemResult = await _mediator.Send(cmd);
            return deleteItemResult.MatchFirst(
                deleted => Ok(new DeleteResponse(deleted)),
                error => error.Type == ErrorType.Validation ? BadRequest(new DeleteResponse(error))
                : Problem(new DeleteResponse(error).ToString())
            );
        }

        [HttpGet("get-all", Name = "get-all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        public async Task<IActionResult> GetAllDepartment()
        {
            var query = new GetAllItemsQuery();
            var getItemsResult = await _mediator.Send(query);
            return getItemsResult.MatchFirst(
                items => Ok(new GetAllResponse<ItemDto>(items)),
                error => error.Type == ErrorType.NotFound ? NotFound(new GetAllResponse<ItemDto>(error))
                : Problem(new GetAllResponse<ItemDto>(error).ToString())
            );
        }

        [HttpGet("get-item/{id}", Name = "get-item")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetDepartmentById(int Id)
        {
            var query = new GetItemByIdQuery(Id);
            var getItemResult = await _mediator.Send(query);
            return getItemResult.MatchFirst(
                item => Ok(new GetByIdResponse<ItemDto>(item)),
                error => error.Type == ErrorType.Validation ? BadRequest(new GetByIdResponse<ItemDto>(error))
                : error.Type == ErrorType.NotFound ? NotFound(new GetByIdResponse<ItemDto>(error))
                : Problem(new GetByIdResponse<ItemDto>(error).ToString())
            );
        }

        [HttpPut("update-item", Name = "update-item")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> UpdateDepartment(UpdateItemRequest request)
        {
            var cmd = new UpdateItemCommand(request.ItemId, request.Price);
            var updateItemResult = await _mediator.Send(cmd);
            return updateItemResult.MatchFirst(
                updated => Ok(new UpdateResponse(updated)),
                error => error.Type == ErrorType.Validation ? BadRequest(new UpdateResponse(error))
                : Problem(new UpdateResponse(error).ToString())
            );
        }

    }
}