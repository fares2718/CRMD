namespace CRMD.Application.InventoryItems.Commands
{
    public record AddInventoryItemCommand(
        int ItemId,
        decimal Quantity,
        decimal MinLevel
    ) : IRequest<ErrorOr<Success>>;
}