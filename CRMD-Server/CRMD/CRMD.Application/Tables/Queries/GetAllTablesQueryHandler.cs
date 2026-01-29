using CRMD.Application.Generics.Queries;
using CRMD.Domain.Tables;

namespace CRMD.Application.Tables.Queries
{
    public class GetAllIneventoryItemsQueryHandler : IRequestHandler<GetAllQuery<TableDto>, ErrorOr<List<TableDto>>>
    {
        private readonly ITableRepository _tableRepository;

        public GetAllIneventoryItemsQueryHandler(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;
        }

        public async Task<ErrorOr<List<TableDto>>> Handle(GetAllQuery<TableDto> request, CancellationToken cancellationToken)
        {
            try
            {
                var tables = await _tableRepository.GetAllTables();
                if (tables == null || tables.Count == 0)
                    return Error.NotFound();
                return tables;

            }
            catch (Exception ex)
            {
                return Error.Failure(ex.Message);
            }
        }
    }
}