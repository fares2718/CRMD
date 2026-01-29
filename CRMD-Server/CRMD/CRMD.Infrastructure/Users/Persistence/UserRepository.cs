using CRMD.Domain.Users;
using CRMD.Infrastructure.Generics;

namespace CRMD.Infrastructure.Users.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddUserAsync(User user)
        {
            await GenericRepository<User>
            .AddAsync(user, _connectionString, "restocafe.adduser");
        }

        public async Task DeleteUserAsync(int userId)
        {
            await GenericRepository<User>
            .DeleteAsync(userId, _connectionString, "restocafe.deleteuser");

        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            var users = new List<UserDto>();
            using (var reader = await GenericRepository<UserDto>
            .GetAllAsync(_connectionString, "restocafe.getusers()"))
            {
                while (await reader.ReadAsync())
                {
                    var user = Mapper.Map<UserDto>(reader);
                    users.Add(user);
                }
                NpgsqlConnection.ClearAllPools();
                return users;
            }
        }

        public async Task<UserDto?> GetUserByIdAsync(int userId)
        {
            using (var reader = await GenericRepository<UserDto>
            .GetByIdAsync(userId, _connectionString, "restocafe.getuserbyid(@id)"))
            {
                UserDto user = new UserDto();
                if (reader != null && await reader.ReadAsync())
                {
                    user = Mapper.Map<UserDto>(reader);
                }
                NpgsqlConnection.ClearAllPools();
                return user;
            }
        }

        public async Task UpdateUserAsync(User newUserData)
        {
            await GenericRepository<User>
            .UpdateAsync(newUserData, _connectionString, "restocafe.updateuser");
        }
    }
}