using CRMD.Application.Employees.Commands;
using CRMD.Application.Employees.Queries;
using CRMD.Contracts.Employees.Delete;
using CRMD.Contracts.Employees.Get;
using CRMD.Contracts.Employees.Post;
using CRMD.Contracts.Employees.Put;
using ErrorOr;

namespace CRMD.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ISender _mediator;

        public EmployeeController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add-employee", Name = "add-employee")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> AddEmployee(AddNewEmployeeRequest request)
        {
            if (request.DepartmentId < 1 || request.Salary < 0
            || string.IsNullOrEmpty(request.Name) || request.Phones.Length == 0
            || request.Roles.Length == 0)
                return BadRequest("Invalid data");

            var employeeRoles = request.Roles.Select(r => (short)r).ToArray();
            var cmd = new AddNewEmployeeCommand(request.Name,
            request.Phones,
            employeeRoles,
            request.Salary,
            request.DepartmentId);

            var addEmployeeResult = await _mediator.Send(cmd);
            return addEmployeeResult.MatchFirst(
                created => CreatedAtRoute("add-employee", Result.Created),
                error => Problem(error.Description)
            );
        }

        [HttpDelete("delete-employee/{id}", Name = "delete-employee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var cmd = new DeleteEmployeeCommand(id);
            var deleteEmployeeResult = await _mediator.Send(cmd);
            return deleteEmployeeResult.MatchFirst(
                deleted => Ok(new DeleteEmployeeResponse(deleted)),
                error => error.Type == ErrorType.Validation ? BadRequest(new DeleteEmployeeResponse(error)) : Problem(new DeleteEmployeeResponse(error).ToString())
            );
        }


        [HttpGet("get-all", Name = "get-all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> GetAllEmployees()
        {
            var query = new GetAllEmployeesQuery();
            var getAllEMployeesResult = await _mediator.Send(query);
            return getAllEMployeesResult.MatchFirst(
                employees => Ok(new GetAllEmployeesResponse(employees)),
                error => Problem(error.Description, error.Code)
            );
        }

        [HttpGet("get-by-id/{Id}", Name = "get-by-id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> GetEmployeeById(int Id)
        {
            var query = new GetEmployeeByIdQuery(Id);
            var getEmployeeByIdResult = await _mediator.Send(query);
            return getEmployeeByIdResult.MatchFirst(
                employee => Ok(new GetEmployeeByIdResponse(employee)),
                error => Problem(error.Description, error.Code)
            );
        }

        [HttpPut("update-salary", Name = "update-salary")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> UpdateEmployeeSalary(UpdateEmployeeSalaryRequest request)
        {
            var cmd = new UpdateEmployeeSalaryCommand(request.Id, request.newSalary);
            var updateResult = await _mediator.Send(cmd);
            return updateResult.MatchFirst(
                updated => Ok(new UpdateEmployeeSalaryResponse(updated)),
                error => Problem(error.Description, error.Code)
            );
        }
    }
}