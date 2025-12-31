namespace CRMD.Contracts.Employees.Post
{
    public record AddNewEmployeeResponse(ErrorOr<Created> Result);
}