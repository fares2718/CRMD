using System;
using CRMD.Domain.Entities;

namespace CRMD.Domain.Repos.Interfaces;

public interface IInventoryRepo
{
    public Task<List<clsInventory>> GetAllInventrotyItemsAsync();
}
