using CRMD.Application.Generics.Queries;
using CRMD.Domain.InventoryItems;

namespace CRMD.Application.Tables.Queries
{
    public class GetTableByIdQueryHandler : IRequestHandler<GetByIdQuery<TableDto>, ErrorOr<TableDto>>
    {
        private readonly ITableRepository _tableRepository;

        public GetTableByIdQueryHandler(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;
        }

        public async Task<ErrorOr<TableDto>> Handle(GetByIdQuery<TableDto> request, CancellationToken cancellationToken)
        {
            if (request.Id < 1)
                return Error.Validation();
            try
            {
                var table = await _tableRepository.GetTableByIdAsync(request.Id);
                if (table == null)
                    return Error.NotFound();
                return table;
            }
            catch (Exception ex)
            {
                return Error.Failure(ex.Message);
            }
        }
    }
}