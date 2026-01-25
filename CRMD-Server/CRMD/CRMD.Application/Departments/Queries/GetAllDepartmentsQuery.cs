using CRMD.Domain.Departments;

namespace CRMD.Application.Departments.Queries
{
    public record GetAllDepartmentsQuery() : IRequest<ErrorOr<List<Department>>>;
}