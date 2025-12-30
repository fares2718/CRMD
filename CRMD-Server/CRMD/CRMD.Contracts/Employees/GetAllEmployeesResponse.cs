namespace CRMD.Contracts.Employees
{
    public record GetAllEmployeesResponse(
        List<EmployeeDto> Employees
    );
}