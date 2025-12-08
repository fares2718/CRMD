using System;
using CRMD.Domain.Entities;

namespace CRMD.Domain.Repos.Interfaces;

public interface IEmployeeRepo
{
    public Task<string> AddEmployeeAsync(clsEmployee employee);
}
