using CRMD.Application.DTOs;

namespace CRMD.Contracts.MenuItems
{
    public record AddNewMenuItemRequest
    (
        string Name,
        RecipeDto Recipe,
        decimal Price,
        short CategoryId
    );
}