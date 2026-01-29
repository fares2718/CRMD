
namespace CRMD.Application.Employees.Commands;

public class UpdateEmployeeSalaryCommandHandler : IRequestHandler<UpdateEmployeeSalaryCommand, ErrorOr<Updated>>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public UpdateEmployeeSalaryCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<Updated>> Handle(UpdateEmployeeSalaryCommand request, CancellationToken cancellationToken)
    {
        if (request.Id < 1 || request.newSalary < 0)
            return Error.Validation();
        var newEmployeeData = _mapper.Map<Employee>(request);
        await _employeeRepository.UpdateEmployeeAsync(newEmployeeData);
        return Result.Updated;
    }
}
