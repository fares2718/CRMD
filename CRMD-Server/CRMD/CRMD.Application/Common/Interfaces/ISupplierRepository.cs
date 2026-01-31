using CRMD.Domain.Suppliers;

namespace CRMD.Application.Common.Interfaces
{
    public interface ISupplierRepository
    {
        Task AddSupplierAsync(Supplier supplier);
        Task DeleteSupplierAsync(int supplierId);
        Task<Supplier?> GetSupplierByIdAsync(int supplierId);
        Task<List<Supplier>?> GetSuppliersAsync();
    }
}