namespace CRMD.Contracts.Tables.Post;

public record AddTablRequest(
    int CategoryId,
    decimal Price,
    string Name
);