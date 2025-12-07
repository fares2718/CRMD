using System;
using System.Data;
using CRMD.Domain.Entities;
using CRMD.Domain.Repos.Interfaces;
using CRMD.Infrastructure.Persistence.Databases;
using Microsoft.Data.SqlClient;

namespace CRMD.Infrastructure.Repositories;

public class RecipeRepo : IRecipeRepo
{
    public async Task<int> AddRecipeAsync(clsRecipe recipe)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_AddRecipe", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MenuItemId", recipe.MenuItemId);
                cmd.Parameters.AddWithValue("@Cost", recipe.Cost);

                var outputIdParam = new SqlParameter("@NewRecipeId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputIdParam);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                return (int)outputIdParam.Value;
            }
        }
    }

    public async Task AddRecipeItemAsync(clsRecipeItem recipeItem)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_AddRecipeItem", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RecipeId", recipeItem.RecipeId);
                cmd.Parameters.AddWithValue("@ItemId", recipeItem.ItemId);
                cmd.Parameters.AddWithValue("@Quantity", recipeItem.Quantity);
                cmd.Parameters.AddWithValue("@Unit", recipeItem.Unit);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task<decimal> CalculateRecipeCostAsync(int recipeId)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_CalculateRecipeCost", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RecipeId", recipeId);

                var outputCostParam = new SqlParameter("@TotalCost", SqlDbType.Decimal)
                {
                    Precision = 18,
                    Scale = 2,
                    Direction = ParameterDirection.ReturnValue
                };
                cmd.Parameters.Add(outputCostParam);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                return (decimal)outputCostParam.Value;
            }
        }
    }

    public async Task<bool> DeleteRecipeAsync(int recipeId)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_DeleteRecipe", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RecipeId", recipeId);

                await conn.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();

                return rowsAffected > 0;
            }
        }
    }

    public async Task<bool> DeleteRecipeItemAsync(int recipeId, int itemId)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_DeleteRecipeItem", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RecipeId", recipeId);
                cmd.Parameters.AddWithValue("@ItemId", itemId);

                await conn.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();

                return rowsAffected > 0;
            }
        }
    }

    public async Task<bool> UpdateRecipeCostAsync(decimal newCost, int recipeId)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_UpdateRecipeCost", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RecipeId", recipeId);
                cmd.Parameters.AddWithValue("@NewCost", newCost);

                await conn.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();

                return rowsAffected > 0;
            }
        }
    }

    public async Task<bool> UpdateRecipeItemQuantityAsync(decimal newQuantity, int recipeId, int itemId)
    {
        using (var conn = SqlConnectionFactory.CreateSqlConnection())
        {
            using (var cmd = new SqlCommand("SP_UpdateRecipeItemQuantity", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@RecipeId", recipeId);
                cmd.Parameters.AddWithValue("@ItemId", itemId);
                cmd.Parameters.AddWithValue("@NewQuantity", newQuantity);

                await conn.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();

                return rowsAffected > 0;
            }
        }
    }
}
