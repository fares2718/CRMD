using CRMD.Domain.Tables;

namespace CRMD.Application.Tables.Commands
{
    public class UpdateTableCommandHandler : IRequestHandler<UpdateTableCommand, ErrorOr<Updated>>
    {
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;

        public UpdateTableCommandHandler(IMapper mapper, ITableRepository tableRepository)
        {

            _mapper = mapper;
            _tableRepository = tableRepository;
        }


        public async Task<ErrorOr<Updated>> Handle(UpdateTableCommand request, CancellationToken cancellationToken)
        {
            if (request.TableId < 1 || request.WaiterId < 1 || request.Capacity < 1)
                return Error.Validation();

            try
            {
                var newTableData = _mapper.Map<Table>(request);
                await _tableRepository.UpdateTableAsync(newTableData);
                return Result.Updated;
            }
            catch (Exception ex)
            {
                return Error.Failure(ex.Message);
            }
        }
    }
}