using CRMD.Application.Items.Commands;
using CRMD.Application.Items.Queries;
using CRMD.Contracts.Items.Delete;
using CRMD.Contracts.Items.Get;
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
                created => CreatedAtRoute("add-item", new AddItemResponse(created)),
                error => error.Type == ErrorType.Validation ? BadRequest(new AddItemResponse(error)) :
                Problem(new AddItemResponse(error).ToString())
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
                deleted => Ok(new DeleteItemResponse(deleted)),
                error => error.Type == ErrorType.Validation ? BadRequest(new DeleteItemResponse(error))
                : Problem(new DeleteItemResponse(error).ToString())
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
                items => Ok(new GetAllItemsResponse(items)),
                error => error.Type == ErrorType.NotFound ? NotFound(new GetAllItemsResponse(error))
                : Problem(new GetAllItemsResponse(error).ToString())
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
                item => Ok(new GetItemByIdResponse(item)),
                error => error.Type == ErrorType.Validation ? BadRequest(new GetItemByIdResponse(error))
                : error.Type == ErrorType.NotFound ? NotFound(new GetItemByIdResponse(error))
                : Problem(new GetItemByIdResponse(error).ToString())
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
                updated => Ok(new UpdateItemResponse(updated)),
                error => error.Type == ErrorType.Validation ? BadRequest(new UpdateItemResponse(error))
                : Problem(new UpdateItemResponse(error).ToString())
            );
        }

    }
}