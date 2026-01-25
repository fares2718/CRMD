namespace CRMD.Contracts.Items.Put;

public record UpdateItemRequest(
    int ItemId,
    decimal Price
);