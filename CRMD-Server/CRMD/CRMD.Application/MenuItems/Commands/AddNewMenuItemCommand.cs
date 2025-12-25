namespace CRMD.Application.MenuItems.Commands;

public record AddNewMenuItemCommand(
    string Name,
    RecipeDto Recipe,
    decimal Price,
    short CategoryId) : IRequest<ErrorOr<Created>>;