namespace CRMD.Contracts.Users.Post;

public record AddUserRequest(
    int EmployeeId,
        string UserName,
        string PasswordHash
);