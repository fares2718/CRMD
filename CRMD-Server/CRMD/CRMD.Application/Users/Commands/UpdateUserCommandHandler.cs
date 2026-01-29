
using CRMD.Domain.Users;

namespace CRMD.Application.Users.Commands
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ErrorOr<Updated>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<Updated>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId < 1 || string.IsNullOrEmpty(request.PasswordHash))
                return Error.Validation("Invalid Data");

            try
            {
                var newUserData = _mapper.Map<User>(request);
                await _userRepository.UpdateUserAsync(newUserData);
                return Result.Updated;
            }
            catch (Exception ex)
            {
                return Error.Failure(ex.Message);
            }
        }
    }
}