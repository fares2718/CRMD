using CRMD.Domain.Departments;

namespace CRMD.Application.Departments.Queries
{
    public record GetDepartmentByIdQuery(int Id) : IRequest<ErrorOr<Department>>;
}