using System;
using CRMD.Domain.Entities;

namespace CRMD.Domain.Repos.Interfaces;

public interface ITableRepo
{
    public Task<int> AddTableAsync(clsTable table);
}
