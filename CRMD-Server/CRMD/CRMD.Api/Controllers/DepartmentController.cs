using CRMD.Application.Departments.Commands;
using CRMD.Contracts.Departments.Delete;
using CRMD.Contracts.Departments.Post;
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
                created => CreatedAtRoute("add-department", new AddDepartmentResponse(created)),
                error => error.Type == ErrorType.Validation ? BadRequest(new AddDepartmentResponse(error)) :
                Problem(new AddDepartmentResponse(error).ToString())
            );
        }

        [HttpDelete("delete-department/{id}", Name = "add-department")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var cmd = new DeleteDepartmentCommand(id);
            var deleteDepartmentResult = await _mediator.Send(cmd);
            return deleteDepartmentResult.MatchFirst(
                deleted => Ok(new DeleteDepartmentResponse(deleted)),
                error => error.Type == ErrorType.NotFound ? NotFound(new DeleteDepartmentResponse(error))
                : Problem(new DeleteDepartmentResponse(error).ToString())
            );
        }
    }
}