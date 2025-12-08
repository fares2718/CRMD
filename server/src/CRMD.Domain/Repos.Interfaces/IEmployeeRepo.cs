using System;
using CRMD.Domain.Entities;

namespace CRMD.Domain.Repos.Interfaces;

public interface IEmployeeRepo
{
    public Task<bool> ActivateEmployeeAsync(string employeeId);
    public Task<string> AddEmployeeAsync(clsEmployee employee);
    public Task<bool> DeactivateEmployeeAsync(string employeeId);
    public Task<bool> UpdateEmployeeSalaryAsync(decimal newSalary, string employeeId);
}
