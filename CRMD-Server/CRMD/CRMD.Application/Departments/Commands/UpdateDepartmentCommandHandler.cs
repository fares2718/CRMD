
using CRMD.Domain.Departments;

namespace CRMD.Application.Departments.Commands
{
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, ErrorOr<Updated>>
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public UpdateDepartmentCommandHandler(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<Updated>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            if (request.Id < 1 || request.employeesCount < 1)
                return Error.Validation();

            try
            {
                var newDepartmentData = _mapper.Map<Department>(request);
                await _departmentRepository.UpdateDepartmentAsync(newDepartmentData);
                return Result.Updated;
            }
            catch (Exception ex)
            {
                return Error.Failure(ex.Message);
            }
        }
    }
}