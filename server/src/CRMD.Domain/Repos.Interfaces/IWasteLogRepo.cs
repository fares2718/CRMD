using System;
using CRMD.Domain.Entities;

namespace CRMD.Domain.Repos.Interfaces;

public interface IWasteLogRepo
{
    public Task<decimal> CalculateItemWasteAsync(int itemId,decimal LeftoverQuantity);
    public Task<int> LogWasteAsync(int itemId, decimal quantity);
    public Task<List<clsWasteLog>> GetWasteLogsReportAsync(DateTime startDate, DateTime endDate);
}
