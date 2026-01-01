namespace CRMD.Contracts.MenuItems.Post;

public record AddNewMenuItemResponse
(
    ErrorOr<Created> Result
);