namespace CRMD.Contracts.MenuItems
{
    public record AddNewMenuItemResponse
    (
        ErrorOr<Created> Result
    );
}