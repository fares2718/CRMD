using System;
using CRMD.Domain.Entities;

namespace CRMD.Domain.Repos.Interfaces;

public interface ICategoryRepo
{
    public Task<int> AddCategoryAsync(clsCategory category);
    public Task<List<clsCategory>> GetAllCategoriesAsync();
    public Task<clsCategory?> GetCategoryById(int categoryId);
}
