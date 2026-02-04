namespace CRMD.Application.MenuItems.Commands
{
    public record UpdateRecipeCommand(
        int RecipeId,
        List<RecipeItemDto> RecipeItems
    ) : IRequest<ErrorOr<Updated>>;
}