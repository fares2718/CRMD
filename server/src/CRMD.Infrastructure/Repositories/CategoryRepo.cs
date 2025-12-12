using System;
using System.Data;
using CRMD.Domain.Entities;
using CRMD.Domain.Repos.Interfaces;
using CRMD.Infrastructure.Persistence.Databases;
using Microsoft.Data.SqlClient;

namespace CRMD.Infrastructure.Repositories;

public class CategoryRepo : ICategoryRepo
{
    public async Task<int> AddCategoryAsync(clsCategory category)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand ("SP_AddCategory",conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name",category.Name);
                var returnedParameter = new SqlParameter("SCOPE_IDENTITY()",SqlDbType.Int)
                {
                    Direction = ParameterDirection.ReturnValue  
                };
                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
                return (int)returnedParameter.Value;
            }
        }
    }

    public async Task<List<clsCategory>> GetAllCategoriesAsync()
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            var categorieList = new List<clsCategory> ();
            using (var cmd = new SqlCommand("SP_GetAllCategories",conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while(await reader.ReadAsync())
                    {
                        var category = new clsCategory
                        {
                          Id = reader.GetInt32(reader.GetOrdinal("Id")),
                          Name = reader.GetString(reader.GetOrdinal("Name"))  
                        };
                        categorieList.Add(category);
                    }
                }
            }
            return categorieList;
        }
    }

    public async Task<clsCategory?> GetCategoryById(int categoryId)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            var categorieList = new List<clsCategory> ();
            using (var cmd = new SqlCommand("SP_GetAllCategories",conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if(await reader.ReadAsync())
                    {
                        var category = new clsCategory
                        {
                          Id = reader.GetInt32(reader.GetOrdinal("Id")),
                          Name = reader.GetString(reader.GetOrdinal("Name"))  
                        };
                        return category;
                    }
                    return null;
                }
            }
    }
    }
}
