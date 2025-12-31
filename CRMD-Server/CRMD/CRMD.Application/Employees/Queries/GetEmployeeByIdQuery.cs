namespace CRMD.Application.Employees.Queries;

public record GetEmployeeByIdQuery(int Id) : IRequest<ErrorOr<EmployeeDto>>;
