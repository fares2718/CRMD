using CRMD.Domain.Suppliers;

namespace CRMD.Application.Common.Interfaces
{
    public interface ISupplierRepository
    {
        Task AddSupplier(Supplier supplier);
        Task DeleteSupplier(int supplierId);
        Task<Supplier?> GetSupplierById(int supplierId);
        Task<List<Supplier>> GetSuppliers();
    }
}