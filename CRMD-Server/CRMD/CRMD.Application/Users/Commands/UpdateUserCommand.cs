namespace CRMD.Application.Users.Commands
{
    public record UpdateUserCommand(
        int UserId,
        string PasswordHash
    ) : IRequest<ErrorOr<Updated>>;
}