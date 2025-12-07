using System;

namespace CRMD.Domain.Entities;

public class clsRecipeItem
{
    public int RecipeId {get;set;}
    public int ItemId {get;set;}
    public decimal Quantity {get;set;}
    public string Unit {get;set;} = null!;
}
