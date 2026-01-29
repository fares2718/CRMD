namespace CRMD.Application.Users.Commands
{
    public record AddUserCommand(
        int EmployeeId,
        string UserName,
        string PasswordHash
    ) : IRequest<ErrorOr<Created>>;
}