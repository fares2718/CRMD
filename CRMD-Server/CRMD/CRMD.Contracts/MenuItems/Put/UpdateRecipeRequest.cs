namespace CRMD.Contracts.MenuItems.Put;

public record UpdateRecipeRequest(
    List<RecipeItemDto> RecipeItems
);
