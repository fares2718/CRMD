namespace CRMD.Contracts.MenuItems.Put;

public record UpdateRecipeRequest(
    int recipeId,
    List<RecipeItemDto> RecipeItems
);
