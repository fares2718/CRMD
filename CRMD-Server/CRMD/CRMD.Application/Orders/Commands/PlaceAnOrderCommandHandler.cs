using MediatR;
using ErrorOr;

namespace CRMD.Application.Orders.Commands;

public class PlaceAnOrderCommandHandler : IRequestHandler<PlaceAnOrderCommand,ErrorOr<int>>
{
    public async Task<ErrorOr<int>> Handle(PlaceAnOrderCommand request, CancellationToken cancellationToken)
    {
        return 1;
    }
}