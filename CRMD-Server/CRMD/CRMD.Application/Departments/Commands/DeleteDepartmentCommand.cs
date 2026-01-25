namespace CRMD.Application.Departments.Commands
{
    public record DeleteDepartmentCommand(int Id) : IRequest<ErrorOr<Deleted>>;
}