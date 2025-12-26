namespace CRMD.Contracts.Employees
{
    public record AddNewEmployeeRequest(
    string Name,
    string[] Phones,
    enRole[] Role,
    decimal Salary,
    int departmentId
    );
}