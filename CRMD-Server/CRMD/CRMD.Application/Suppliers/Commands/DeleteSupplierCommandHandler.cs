
namespace CRMD.Application.Suppliers.Commands
{
    public class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand, ErrorOr<Deleted>>
    {
        private readonly ISupplierRepository _supplierRepository;

        public DeleteSupplierCommandHandler(ISupplierRepository supplierRepository) => _supplierRepository = supplierRepository;

        public async Task<ErrorOr<Deleted>> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            if (request.supplierId < 0)
                return Error.Validation();

            await _supplierRepository.DeleteSupplierAsync(request.supplierId);
            return Result.Deleted;
        }
    }
}