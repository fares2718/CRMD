using CRMD.Domain.Departments;

namespace CRMD.Application.Departments.Queries
{
    public class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, ErrorOr<Department>>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public GetDepartmentByIdQueryHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<ErrorOr<Department>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id < 1)
                return Error.Validation();

            try
            {
                var department = await _departmentRepository.GetDepartmentByIdAsync(request.Id);
                if (department == null)
                    return Error.NotFound();

                return department;
            }
            catch (Exception ex)
            {
                return Error.Failure(ex.Message);
            }
        }
    }
}