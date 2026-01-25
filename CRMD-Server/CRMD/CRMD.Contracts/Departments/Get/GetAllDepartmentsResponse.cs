using CRMD.Domain.Departments;

namespace CRMD.Contracts.Departments.Get
{
    public record GetAllDepartmentsResponse(ErrorOr<List<Department>> response);
}