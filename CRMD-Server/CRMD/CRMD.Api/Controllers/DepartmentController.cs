using CRMD.Application.Departments.Commands;
using CRMD.Application.Departments.Queries;
using CRMD.Contracts.Departments.Delete;
using CRMD.Contracts.Departments.Get;
using CRMD.Contracts.Departments.Post;
using CRMD.Contracts.Departments.Put;
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
                deleted => Ok(new DeleteDepartmentResponse(deleted)),
                error => error.Type == ErrorType.Validation ? BadRequest(new DeleteDepartmentResponse(error))
                : Problem(new DeleteDepartmentResponse(error).ToString())
            );
        }

        [HttpGet("get-all", Name = "get-all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        public async Task<IActionResult> GetAllDepartment()
        {
            var query = new GetAllDepartmentsQuery();
            var getDepartmentsResult = await _mediator.Send(query);
            return getDepartmentsResult.MatchFirst(
                departments => Ok(new GetAllDepartmentsResponse(departments)),
                error => error.Type == ErrorType.NotFound ? NotFound(new GetAllDepartmentsResponse(error))
                : Problem(new GetAllDepartmentsResponse(error).ToString())
            );
        }

        [HttpGet("get-department/{id}", Name = "get-department")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetDepartmentById(int Id)
        {
            var query = new GetDepartmentByIdQuery(Id);
            var getDepartmentResult = await _mediator.Send(query);
            return getDepartmentResult.MatchFirst(
                department => Ok(new GetDepartmentByIdResponse(department)),
                error => error.Type == ErrorType.Validation ? BadRequest(new GetDepartmentByIdResponse(error))
                : error.Type == ErrorType.NotFound ? NotFound(new GetDepartmentByIdResponse(error))
                : Problem(new GetDepartmentByIdResponse(error).ToString())
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
                updated => Ok(new UpdateDepartmentResponse(updated)),
                error => error.Type == ErrorType.Validation ? BadRequest(new UpdateDepartmentResponse(error))
                : Problem(new UpdateDepartmentResponse(error).ToString())
            );
        }


    }
}