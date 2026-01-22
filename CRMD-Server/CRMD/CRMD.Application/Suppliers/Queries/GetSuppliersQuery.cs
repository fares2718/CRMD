using CRMD.Domain.Suppliers;

namespace CRMD.Application.Suppliers.Queries
{
    public record GetSuppliersQuery() : IRequest<ErrorOr<List<Supplier>>>;
}