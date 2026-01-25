namespace CRMD.Contracts.Departments.Put
{
    public record UpdateDepartmentRequest(
        int Id,
        short employeesCount
    );
}