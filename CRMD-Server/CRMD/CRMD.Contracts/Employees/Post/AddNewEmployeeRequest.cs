namespace CRMD.Contracts.Employees.Post
{
    public record AddNewEmployeeRequest(
    string Name,
    string[] Phones,
    enRole[] Roles,
    decimal Salary,
    int DepartmentId
    );
}