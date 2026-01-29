using CRMD.Application.Generics.Commands;
using CRMD.Domain.Tables;

namespace CRMD.Application.Tables.Commands
{
    public class DeleteTableCommandHandler : IRequestHandler<DeleteCommand<Table>, ErrorOr<Deleted>>
    {
        private readonly ITableRepository _tableRepository;

        public DeleteTableCommandHandler(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;
        }

        public async Task<ErrorOr<Deleted>> Handle(DeleteCommand<Table> request, CancellationToken cancellationToken)
        {
            if (request.Id < 1)
                return Error.Validation();

            try
            {
                await _tableRepository.DeleteTableAsync(request.Id);
                return Result.Deleted;
            }
            catch (Exception ex)
            {
                return Error.Failure(ex.Message);
            }
        }
    }
}