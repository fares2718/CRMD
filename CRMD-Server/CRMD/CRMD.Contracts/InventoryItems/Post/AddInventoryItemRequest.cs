namespace CRMD.Contracts.InventoryItems.Post;

public record AddInventoryItemRequest(
    int ItemId,
        decimal Quantity,
        decimal MinLevel
);