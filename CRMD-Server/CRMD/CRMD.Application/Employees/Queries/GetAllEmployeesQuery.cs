namespace CRMD.Application.Employees.Queries
{
    public record GetAllEmployeesQuery() : IRequest<ErrorOr<List<EmployeeDto>>>;
}