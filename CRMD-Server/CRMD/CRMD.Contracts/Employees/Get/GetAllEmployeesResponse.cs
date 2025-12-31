namespace CRMD.Contracts.Employees.Get
{
    public record GetAllEmployeesResponse(
        List<EmployeeDto> Employees
    );
}