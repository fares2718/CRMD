using CRMD.Application.Generics.Queries;

namespace CRMD.Application.Users.Queries
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetByIdQuery<UserDto>, ErrorOr<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<UserDto>> Handle(GetByIdQuery<UserDto> request, CancellationToken cancellationToken)
        {
            if (request.Id < 1)
                return Error.Validation();
            try
            {
                var user = await _userRepository.GetUserByIdAsync(request.Id);
                if (user == null)
                    return Error.NotFound();
                return user;
            }
            catch (Exception ex)
            {
                return Error.Failure(ex.Message);
            }
        }
    }
}