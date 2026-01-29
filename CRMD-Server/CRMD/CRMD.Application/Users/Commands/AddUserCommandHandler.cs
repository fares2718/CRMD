
using CRMD.Domain.Users;

namespace CRMD.Application.Users.Commands
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, ErrorOr<Created>>
    {
        private readonly IUserRepository _userRepositroy;
        private readonly IMapper _mapper;

        public AddUserCommandHandler(IUserRepository userRepositroy, IMapper mapper)
        {
            _userRepositroy = userRepositroy;
            _mapper = mapper;
        }

        public async Task<ErrorOr<Created>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            if (request.EmployeeId < 1 || string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.PasswordHash))
                return Error.Validation("Invalid Data");
            try
            {
                var user = _mapper.Map<User>(request);
                await _userRepositroy.AddUserAsync(user);
                return Result.Created;
            }
            catch (Exception ex)
            {
                return Error.Failure(ex.Message);
            }
        }
    }
}