using CRMD.Domain.Tables;

namespace CRMD.Application.Tables.Commands
{
    public class AddTableCommandHandler : IRequestHandler<AddTableCommand, ErrorOr<Created>>
    {
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;

        public AddTableCommandHandler(IMapper mapper, ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<Created>> Handle(AddTableCommand request, CancellationToken cancellationToken)
        {
            if (request.SectionId < 1 || request.WaiterId < 1 || request.Capacity < 1)
                return Error.Validation();

            var table = _mapper.Map<Table>(request);
            try
            {
                await _tableRepository.AddTableAsync(table);
                return Result.Created;
            }
            catch (Exception ex)
            {
                return Error.Failure(ex.Message);
            }
        }
    }
}