using CRMD.Application.DTOs;
using CRMD.Application.Generics.Commands;
using CRMD.Application.Generics.Queries;
using CRMD.Application.Users.Commands;
using CRMD.Contracts.Users.Post;
using CRMD.Contracts.Users.Put;
using CRMD.Domain.Users;
using ErrorOr;

namespace CRMD.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ISender _mediator;

        public UserController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add-user", Name = "add-user")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> AddUser(AddUserRequest request)
        {
            var cmd = new AddUserCommand(request.EmployeeId, request.UserName, request.PasswordHash);
            var addUserResult = await _mediator.Send(cmd);
            return addUserResult.MatchFirst(
                created => CreatedAtRoute("add-user", new AddResponse(created)),
                error => error.Type == ErrorType.Validation ? BadRequest(new AddResponse(error)) :
                Problem(new AddResponse(error).ToString())
            );
        }

        [HttpDelete("delete-user/{id}", Name = "delete-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var cmd = new DeleteCommand<User>(id);
            var deleteUserResult = await _mediator.Send(cmd);
            return deleteUserResult.MatchFirst(
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
            var query = new GetAllQuery<UserDto>();
            var getUsersResult = await _mediator.Send(query);
            return getUsersResult.MatchFirst(
                Users => Ok(new GetAllResponse<UserDto>(Users)),
                error => error.Type == ErrorType.NotFound ? NotFound(new GetAllResponse<UserDto>(error))
                : Problem(new GetAllResponse<UserDto>(error).ToString())
            );
        }

        [HttpGet("get-User/{id}", Name = "get-User")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetDepartmentById(int Id)
        {
            var query = new GetByIdQuery<UserDto>(Id);
            var getUserResult = await _mediator.Send(query);
            return getUserResult.MatchFirst(
                User => Ok(new GetByIdResponse<UserDto>(User)),
                error => error.Type == ErrorType.Validation ? BadRequest(new GetByIdResponse<UserDto>(error))
                : error.Type == ErrorType.NotFound ? NotFound(new GetByIdResponse<UserDto>(error))
                : Problem(new GetByIdResponse<UserDto>(error).ToString())
            );
        }

        [HttpPut("update-user", Name = "update-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> UpdateDepartment(UpdateUserRequest request)
        {
            var cmd = new UpdateUserCommand(request.UserId, request.PasswordHash);
            var updateUserResult = await _mediator.Send(cmd);
            return updateUserResult.MatchFirst(
                updated => Ok(new UpdateResponse(updated)),
                error => error.Type == ErrorType.Validation ? BadRequest(new UpdateResponse(error))
                : Problem(new UpdateResponse(error).ToString())
            );
        }
    }
}