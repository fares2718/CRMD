namespace CRMD.Contracts.MenuItems
{
    public record UpdateRecipeResponse
    (
        ErrorOr<Updated> Result
    );
}