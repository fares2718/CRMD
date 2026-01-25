namespace CRMD.Application.Departments.Commands
{
    public record AddDepartmentCommand(
        string Name,
        short employeesCount
    ) : IRequest<ErrorOr<Created>>;
}