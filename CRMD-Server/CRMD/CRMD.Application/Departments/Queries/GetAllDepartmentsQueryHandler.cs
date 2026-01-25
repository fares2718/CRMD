using CRMD.Domain.Departments;

namespace CRMD.Application.Departments.Queries
{
    public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, ErrorOr<List<Department>>>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public GetAllDepartmentsQueryHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<ErrorOr<List<Department>>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var departments = await _departmentRepository.GetAllDepartmentsAsync();
                if (departments == null || departments.Count == 0)
                    return Error.NotFound();
                return departments;
            }
            catch (Exception ex)
            {
                return Error.Failure(ex.Message);
            }
        }
    }
}