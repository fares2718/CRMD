namespace CRMD.Contracts.Tables.Put;

public record UpdateTableRequest(
    int TableId,
        int WaiterId,
        short Capacity
);