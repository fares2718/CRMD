using CRMD.Application.Generics.Commands;
using CRMD.Domain.Users;

namespace CRMD.Application.Users.Commands
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteCommand<User>, ErrorOr<Deleted>>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<Deleted>> Handle(DeleteCommand<User> request, CancellationToken cancellationToken)
        {
            if (request.Id < 1)
                return Error.Validation("Invalid Data");

            try
            {
                await _userRepository.DeleteUserAsync(request.Id);
                return Result.Deleted;
            }
            catch (Exception ex)
            {
                return Error.Failure(ex.Message);
            }
        }
    }
}