namespace CRMD.Contracts.Tables.Put;

public record UpdateTableRequest(
    int ItemId,
    decimal Price
);