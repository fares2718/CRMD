namespace CRMD.Contracts.MenuItems.Post;

public record AddNewMenuItemRequest
(
    string Name,
    RecipeDto Recipe,
    decimal Price,
    short CategoryId
);
