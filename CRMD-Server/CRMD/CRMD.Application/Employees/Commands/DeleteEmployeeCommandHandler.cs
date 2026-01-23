using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMD.Application.Employees.Commands
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, ErrorOr<Deleted>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<ErrorOr<Deleted>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            if (request.id < 1)
                return Error.Validation();

            try
            {
                await _employeeRepository.DeleteEmployee(request.id);
                return Result.Deleted;
            }
            catch (Exception ex)
            {
                return Error.Failure(ex.Message);
            }
        }
    }
}