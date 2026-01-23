namespace CRMD.Contracts.Employees.Delete
{
    public record DeleteEmployeeResponse(ErrorOr<Deleted> response);
}