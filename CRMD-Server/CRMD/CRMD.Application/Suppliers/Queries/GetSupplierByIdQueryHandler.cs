using CRMD.Domain.Suppliers;

namespace CRMD.Application.Suppliers.Queries
{
    public class GetSupplierByIdQueryHandler : IRequestHandler<GetSupplierByIdQuery, ErrorOr<Supplier>>
    {
        private readonly ISupplierRepository _supplierRepository;

        public GetSupplierByIdQueryHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<ErrorOr<Supplier>> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var supplier = await _supplierRepository.GetSupplierById(request.Id);
                if (supplier == null)
                    return Error.NotFound();

                return supplier;
            }
            catch (Exception ex)
            {
                return Error.Forbidden(ex.Message);
            }
        }
    }
}