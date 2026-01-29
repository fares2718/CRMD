namespace CRMD.Application.InventoryItems.Commands
{
    public record UpdateInventoryItemCommand(
        int ItemId,
        decimal Quantity
    ) : IRequest<ErrorOr<Updated>>;
}