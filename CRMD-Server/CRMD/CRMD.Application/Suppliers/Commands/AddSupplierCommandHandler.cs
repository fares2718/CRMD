
using CRMD.Domain.Suppliers;

namespace CRMD.Application.Suppliers.Commands
{
    public class AddSupplierCommandHandler : IRequestHandler<AddSupplierCommand, ErrorOr<Created>>
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public AddSupplierCommandHandler(ISupplierRepository supplierRepository, IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<Created>> Handle(AddSupplierCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.name) || string.IsNullOrEmpty(request.address)
            || request.phones.Length == 0)
                return Error.Validation();

            var supplier = _mapper.Map<Supplier>(request);
            await _supplierRepository.AddSupplier(supplier);
            return Result.Created;
        }
    }
}