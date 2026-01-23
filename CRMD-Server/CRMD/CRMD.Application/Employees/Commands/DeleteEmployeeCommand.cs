using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMD.Application.Employees.Commands
{
    public record DeleteEmployeeCommand(int id) : IRequest<ErrorOr<Deleted>>;
}