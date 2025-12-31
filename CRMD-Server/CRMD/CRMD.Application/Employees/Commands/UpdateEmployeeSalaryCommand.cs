namespace CRMD.Application.Employees.Commands;

public record UpdateEmployeeSalaryCommand(int Id, decimal newSalary) : IRequest<ErrorOr<Updated>>;
