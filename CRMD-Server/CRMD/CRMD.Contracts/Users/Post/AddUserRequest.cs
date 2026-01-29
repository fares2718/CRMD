namespace CRMD.Contracts.Users.Post;

public record AddUserRequest(
    int CategoryId,
    decimal Price,
    string Name
);