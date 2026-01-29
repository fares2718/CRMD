namespace CRMD.Contracts.Users.Put;

public record UpdateUserRequest(
    int UserId,
        string PasswordHash
);