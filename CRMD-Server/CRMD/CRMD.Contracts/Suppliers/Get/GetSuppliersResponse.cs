using CRMD.Domain.Suppliers;

namespace CRMD.Contracts.Suppliers.Get
{
    public record GetSuppliersResponse(ErrorOr<List<Supplier>> response);
}