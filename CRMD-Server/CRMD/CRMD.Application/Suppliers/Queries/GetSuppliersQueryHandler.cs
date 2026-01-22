using CRMD.Domain.Suppliers;

namespace CRMD.Application.Suppliers.Queries
{
    public class GetSuppliersQueryHandler : IRequestHandler<GetSuppliersQuery, ErrorOr<List<Supplier>>>
    {
        private readonly ISupplierRepository _supplierRepository;

        public GetSuppliersQueryHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<ErrorOr<List<Supplier>>> Handle(GetSuppliersQuery request, CancellationToken cancellationToken)
        {
            var Suppliers = await _supplierRepository.GetSuppliers();
            if (Suppliers.Count == 0)
                return Error.NotFound();
            return Suppliers;
        }
    }
}