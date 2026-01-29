namespace CRMD.Contracts.InventoryItems.Post;

public record AddInventoryItemRequest(
    int CategoryId,
    decimal Price,
    string Name
);