
namespace CRMD.Application.Employees.Commands;

public class UpdateEmployeeSalaryCommandHandler : IRequestHandler<UpdateEmployeeSalaryCommand, ErrorOr<Updated>>
{
    private readonly IEmployeeRepository _employeeRepository;

    public UpdateEmployeeSalaryCommandHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<ErrorOr<Updated>> Handle(UpdateEmployeeSalaryCommand request, CancellationToken cancellationToken)
    {
        if (request.Id < 1 || request.newSalary < 0)
            return Error.Validation();
        await _employeeRepository.UpdateEmployeeSalary(request.Id, request.newSalary);
        return Result.Updated;
    }
}
