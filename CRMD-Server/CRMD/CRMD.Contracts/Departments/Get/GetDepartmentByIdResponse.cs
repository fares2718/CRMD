using CRMD.Domain.Departments;

namespace CRMD.Contracts.Departments.Get
{
    public record GetDepartmentByIdResponse(ErrorOr<Department> department);
}