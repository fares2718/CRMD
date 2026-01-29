namespace CRMD.Contracts.Users.Put;

public record UpdateUserRequest(
    int ItemId,
    decimal Price
);