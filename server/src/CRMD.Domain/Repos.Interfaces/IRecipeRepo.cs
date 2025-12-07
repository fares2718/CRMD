using System;
using CRMD.Domain.Entities;

namespace CRMD.Domain.Repos.Interfaces;

public interface IRecipeRepo
{
    public Task<int> AddRecipeAsync(clsRecipe recipe);
    public Task AddRecipeItemAsync(clsRecipeItem recipeItem);
    public Task<decimal> CalculateRecipeCostAsync(int recipeId);
    public Task<bool> DeleteRecipeAsync(int recipeId);
    public Task<bool> DeleteRecipeItemAsync(int recipeId, int itemId);
    public Task<bool> UpdateRecipeCostAsync(decimal newCost, int recipeId);
    public Task<bool> UpdateRecipeItemQuantityAsync(decimal newQuantity, int recipeId, int itemId);
}
