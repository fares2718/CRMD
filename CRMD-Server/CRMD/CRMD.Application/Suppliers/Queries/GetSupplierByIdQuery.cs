using CRMD.Domain.Suppliers;

namespace CRMD.Application.Suppliers.Queries
{
    public record GetSupplierByIdQuery(int Id) : IRequest<ErrorOr<Supplier>>;
}