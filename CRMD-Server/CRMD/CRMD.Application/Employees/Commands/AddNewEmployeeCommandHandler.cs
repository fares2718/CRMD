namespace CRMD.Application.Employees.Commands
{
    public class AddNewEmployeeCommandHandler : IRequestHandler<AddNewEmployeeCommand, ErrorOr<Created>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public AddNewEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<Created>> Handle(AddNewEmployeeCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Name) || request.Salary <= 0 ||
            request.Roles.Length == 0 || request.Phones.Length == 0)
                return Error.Validation();
            var employee = _mapper.Map<Employee>(request);
            await _employeeRepository.AddNewEmployeeAsync(employee);
            return Result.Created;
        }
    }
}