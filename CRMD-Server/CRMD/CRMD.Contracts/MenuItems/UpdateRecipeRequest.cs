namespace CRMD.Contracts.MenuItems
{
    public record UpdateRecipeRequest(
        List<RecipeItemDto> RecipeItems
    );
}