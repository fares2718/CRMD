


namespace CRMD.Application.Employees.Queries;

public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, ErrorOr<EmployeeDto>>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<ErrorOr<EmployeeDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetEmployeeByIdAsync(request.Id);
        if (string.IsNullOrEmpty(employee.Name) || !employee.Roles.Any()
        || !employee.Phones.Any() || string.IsNullOrEmpty(employee.Department))
        {
            return Error.NotFound();
        }
        return employee;
    }
}
