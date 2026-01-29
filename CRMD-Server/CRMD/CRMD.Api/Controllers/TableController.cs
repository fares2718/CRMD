using CRMD.Application.DTOs;
using CRMD.Application.Generics.Commands;
using CRMD.Application.Generics.Queries;
using CRMD.Application.Tables.Commands;
using CRMD.Contracts.Tables.Post;
using CRMD.Contracts.Tables.Put;
using CRMD.Domain.Tables;
using ErrorOr;

namespace CRMD.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TableController : ControllerBase
    {
        private readonly ISender _mediator;

        public TableController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add-Table", Name = "add-Table")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> AddTable(AddTablRequest request)
        {
            var cmd = new AddTableCommand(request.SectionId, request.WaiterId, request.Capacity);
            var addTableResult = await _mediator.Send(cmd);
            return addTableResult.MatchFirst(
                created => CreatedAtRoute("add-Table", new AddResponse(created)),
                error => error.Type == ErrorType.Validation ? BadRequest(new AddResponse(error)) :
                Problem(new AddResponse(error).ToString())
            );
        }

        [HttpDelete("delete-Table/{id}", Name = "delete-Table")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var cmd = new DeleteCommand<Table>(id);
            var deleteTableResult = await _mediator.Send(cmd);
            return deleteTableResult.MatchFirst(
                deleted => Ok(new DeleteResponse(deleted)),
                error => error.Type == ErrorType.Validation ? BadRequest(new DeleteResponse(error))
                : Problem(new DeleteResponse(error).ToString())
            );
        }

        [HttpGet("get-tables", Name = "get-tables")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        public async Task<IActionResult> GetAllDepartment()
        {
            var query = new GetAllQuery<TableDto>();
            var getTablesResult = await _mediator.Send(query);
            return getTablesResult.MatchFirst(
                Tables => Ok(new GetAllResponse<TableDto>(Tables)),
                error => error.Type == ErrorType.NotFound ? NotFound(new GetAllResponse<TableDto>(error))
                : Problem(new GetAllResponse<TableDto>(error).ToString())
            );
        }

        [HttpGet("get-Table/{id}", Name = "get-Table")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetDepartmentById(int Id)
        {
            var query = new GetByIdQuery<TableDto>(Id);
            var getTableResult = await _mediator.Send(query);
            return getTableResult.MatchFirst(
                Table => Ok(new GetByIdResponse<TableDto>(Table)),
                error => error.Type == ErrorType.Validation ? BadRequest(new GetByIdResponse<TableDto>(error))
                : error.Type == ErrorType.NotFound ? NotFound(new GetByIdResponse<TableDto>(error))
                : Problem(new GetByIdResponse<TableDto>(error).ToString())
            );
        }

        [HttpPut("update-Table", Name = "update-Table")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> UpdateDepartment(UpdateTableRequest request)
        {
            var cmd = new UpdateTableCommand(request.TableId, request.WaiterId, request.Capacity);
            var updateTableResult = await _mediator.Send(cmd);
            return updateTableResult.MatchFirst(
                updated => Ok(new UpdateResponse(updated)),
                error => error.Type == ErrorType.Validation ? BadRequest(new UpdateResponse(error))
                : Problem(new UpdateResponse(error).ToString())
            );
        }
    }
}