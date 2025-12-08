using System;
using CRMD.Domain.Entities;

namespace CRMD.Domain.Repos.Interfaces;

public interface IItemsRepo
{
    public Task<int> AddItemAsync(clsItem item);
    public Task<int> AddItemCategoryAsync(string category);
    public Task<bool> UpdateItemCostAsync(int itemId, decimal newCost);
}
