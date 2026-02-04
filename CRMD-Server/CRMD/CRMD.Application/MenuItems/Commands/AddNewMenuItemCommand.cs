namespace CRMD.Application.MenuItems.Commands;

public record AddNewMenuItemCommand(
    string Name,
    List<RecipeItemDto> Ingredients,
    decimal Price,
    short CategoryId) : IRequest<ErrorOr<Created>>;