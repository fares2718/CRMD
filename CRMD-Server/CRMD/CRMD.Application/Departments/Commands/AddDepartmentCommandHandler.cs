
using CRMD.Domain.Departments;

namespace CRMD.Application.Departments.Commands
{
    public class AddDepartmentCommandHandler : IRequestHandler<AddDepartmentCommand, ErrorOr<Created>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public AddDepartmentCommandHandler(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<Created>> Handle(AddDepartmentCommand request, CancellationToken cancellationToken)
        {
            if (request.employeesCount < 1 || string.IsNullOrEmpty(request.Name))
                return Error.Validation();

            var department = _mapper.Map<Department>(request);
            try
            {
                await _departmentRepository.AddNewDepartmentAsync(department);
                return Result.Created;
            }
            catch (Exception ex)
            {
                return Error.Failure(ex.Message);
            }
        }
    }
}