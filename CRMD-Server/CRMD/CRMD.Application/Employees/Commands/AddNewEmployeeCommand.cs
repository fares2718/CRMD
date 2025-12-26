namespace CRMD.Application.Employees.Commands
{


    public record AddNewEmployeeCommand(
        string Name,
        string[] Phones,
        short[] Roles,
        decimal Salary,
        int departmentId
    ) : IRequest<ErrorOr<Created>>;

}