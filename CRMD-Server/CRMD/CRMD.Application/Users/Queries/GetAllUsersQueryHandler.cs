using CRMD.Application.Generics.Queries;

namespace CRMD.Application.Users.Queries
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllQuery<UserDto>, ErrorOr<List<UserDto>>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<List<UserDto>>> Handle(GetAllQuery<UserDto> request, CancellationToken cancellationToken)
        {
            try
            {
                var users = await _userRepository.GetAllUsers();
                if (users == null || users.Count == 0)
                    return Error.NotFound();
                return users;

            }
            catch (Exception ex)
            {
                return Error.Failure(ex.Message);
            }
        }
    }
}