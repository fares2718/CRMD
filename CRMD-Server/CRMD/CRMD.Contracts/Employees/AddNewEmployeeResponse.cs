namespace CRMD.Contracts.Employees
{
    public record AddNewEmployeeResponse(ErrorOr<Created> Result);
}