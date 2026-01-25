namespace CRMD.Application.Departments.Commands
{
    public record UpdateDepartmentCommand(
        int Id,
        short employeesCount
    ) : IRequest<ErrorOr<Updated>>;
}