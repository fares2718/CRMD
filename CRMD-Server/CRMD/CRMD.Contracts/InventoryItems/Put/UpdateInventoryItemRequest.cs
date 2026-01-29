namespace CRMD.Contracts.InventoryItems.Put;

public record UpdateInventoryItemRequest(
    int ItemId,
    decimal Quantity
);