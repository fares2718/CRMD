namespace CRMD.Contracts.Items.Post;

public record AddItemRequest(
    int CategoryId,
    decimal Price,
    string Name
);