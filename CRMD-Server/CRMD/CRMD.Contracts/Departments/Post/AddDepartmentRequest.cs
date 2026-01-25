namespace CRMD.Contracts.Departments.Post
{
    public record AddDepartmentRequest(
        string Name,
        short employeesCount
    );
}