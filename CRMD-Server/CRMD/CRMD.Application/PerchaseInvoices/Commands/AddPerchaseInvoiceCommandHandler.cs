
using CRMD.Domain.PerchaseInvoices;

namespace CRMD.Application.PerchaseInvoices.Commands
{
    public class AddPerchaseInvoiceCommandHandler : IRequestHandler<AddPerchaseInvoiceCommand, ErrorOr<Created>>
    {
        private readonly IPerchaseInvoiceRepository _perchaseInvoiceRepository;
        private readonly IMapper _mapper;

        public AddPerchaseInvoiceCommandHandler(IPerchaseInvoiceRepository perchaseInvoiceRepository, IMapper mapper)
        {
            _perchaseInvoiceRepository = perchaseInvoiceRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<Created>> Handle(AddPerchaseInvoiceCommand request, CancellationToken cancellationToken)
        {
            if (request.supplierId < 1 || request.totalAmount < 0)
                return Error.Validation();
            var invoice = _mapper.Map<PerchaseInvoice>(request);
            await _perchaseInvoiceRepository.AddPerchaseInvoiceAsync(invoice);
            return Result.Created;
        }
    }
}