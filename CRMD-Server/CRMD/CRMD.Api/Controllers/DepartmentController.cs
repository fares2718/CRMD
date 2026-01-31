using CRMD.Application.Departments.Commands;
using CRMD.Application.Departments.Queries;
using CRMD.Contracts.Departments.Post;
using CRMD.Contracts.Departments.Put;
using CRMD.Domain.Departments;
using ErrorOr;

namespace CRMD.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly ISender _mediator;

        public DepartmentController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add-department", Name = "add-department")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> AddDepartment(AddDepartmentRequest request)
        {
            var cmd = new AddDepartmentCommand(request.Name, request.employeesCount);
            var addDepartmentResult = await _mediator.Send(cmd);
            return addDepartmentResult.MatchFirst(
                created => CreatedAtRoute("add-department", new AddResponse(created)),
                error => error.Type == ErrorType.Validation ? BadRequest(new AddResponse(error)) :
                Problem(new AddResponse(error).ToString())
            );
        }

        [HttpDelete("delete-department/{id}", Name = "delete-department")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var cmd = new DeleteDepartmentCommand(id);
            var deleteDepartmentResult = await _mediator.Send(cmd);
            return deleteDepartmentResult.MatchFirst(
                deleted => Ok(new DeleteResponse(deleted)),
                error => error.Type == ErrorType.Validation ? BadRequest(new DeleteResponse(error))
                : Problem(new DeleteResponse(error).ToString())
            );
        }

        [HttpGet("get-departments", Name = "get-departments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        public async Task<IActionResult> GetAllDepartment()
        {
            var query = new GetAllDepartmentsQuery();
            var getDepartmentsResult = await _mediator.Send(query);
            return getDepartmentsResult.MatchFirst(
                departments => Ok(new GetAllResponse<Department>(departments)),
                error => error.Type == ErrorType.NotFound ? NotFound(new GetAllResponse<Department>(error))
                : Problem(new GetAllResponse<Department>(error).ToString())
            );
        }

        [HttpGet("get-department/{id}", Name = "get-department")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var query = new GetDepartmentByIdQuery(id);
            var getDepartmentResult = await _mediator.Send(query);
            return getDepartmentResult.MatchFirst(
                department => Ok(new GetByIdResponse<Department>(department)),
                error => error.Type == ErrorType.Validation ? BadRequest(new GetByIdResponse<Department>(error))
                : error.Type == ErrorType.NotFound ? NotFound(new GetByIdResponse<Department>(error))
                : Problem(new GetByIdResponse<Department>(error).ToString())
            );
        }

        [HttpPut("update-department", Name = "update-department")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> UpdateDepartment(UpdateDepartmentRequest request)
        {
            var cmd = new UpdateDepartmentCommand(request.Id, request.employeesCount);
            var updateDepartmentResult = await _mediator.Send(cmd);
            return updateDepartmentResult.MatchFirst(
                updated => Ok(new UpdateResponse(updated)),
                error => error.Type == ErrorType.Validation ? BadRequest(new UpdateResponse(error))
                : Problem(new UpdateResponse(error).ToString())
            );
        }


    }
}