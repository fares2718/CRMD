using CRMD.Application.Employees.Commands;
using CRMD.Contracts.Employees;
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
    }
}