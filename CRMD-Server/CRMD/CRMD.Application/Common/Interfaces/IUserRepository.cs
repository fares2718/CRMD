using CRMD.Domain.Users;

namespace CRMD.Application.Common.Interfaces
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task DeleteUserAsync(int userId);
        Task<List<UserDto>> GetAllUsers();
        Task<UserDto?> GetUserByIdAsync(int userId);
        Task UpdateUserAsync(User newUserData);
    }
}