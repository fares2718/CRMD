using System;
using CRMD.Domain.Entities;

namespace CRMD.Domain.Repos.Interfaces;

public interface IDailyConsumption
{
    public Task<int> AddDailyConsumptionAsync(Queue<clsDailyConsumption> dailyConsumptions);
    public Task<List<clsDailyConsumption>> GetAllDailyConsumptions();
    public Task<List<clsDailyConsumption>> GetDailyConsumptionByDateAsync(DateTime conDate);
}
