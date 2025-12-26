namespace CRMD.Contracts.Employees
{
    public record AddNewEmployeeRequest(
    string Name,
    string[] Phones,
    enRole[] Roles,
    decimal Salary,
    int DepartmentId
    );
}