using CRMD.Domain.Suppliers;

namespace CRMD.Contracts.Suppliers.Get
{
    public record GetSupplierByIdResponse(ErrorOr<Supplier> response);
}