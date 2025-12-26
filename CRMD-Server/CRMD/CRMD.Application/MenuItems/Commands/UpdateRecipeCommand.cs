namespace CRMD.Application.MenuItems.Commands
{
    public record UpdateRecipeCommand(
        List<RecipeItemDto> RecipeItems
    ) : IRequest<ErrorOr<Updated>>;
}