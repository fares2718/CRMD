using System;
using CRMD.Domain.Entities;

namespace CRMD.Domain.Repos.Interfaces;

public interface IDiscountRepo
{
    public Task<bool> ActivatePromocodeAsync(string promocode);
    public Task<int> AddDiscountAsync(clsDiscount discount);
    public Task<bool> ApplyDiscountAsync(int discountId, int orderId);
    public Task<bool> DeactivatePromocodeAsync(string promocode);
    public Task<List<clsDiscount>> GetActivePromocodesAsync();
}
