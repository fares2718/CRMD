using System;

namespace CRMD.Domain.Repos.Interfaces;

public interface IWasteLogRepo
{
    public Task<decimal> CalculateItemWasteAsync(int itemId,decimal LeftoverQuantity);
    public Task<int> LogWasteAsync(int itemId, decimal quantity);
}
