using MediatR;

namespace CRMD.Application.Orders.Commands;

public class PlaceAnOrderCommandHandler : IRequestHandler<PlaceAnOrderCommand,int>
{
    public Task<int> Handle(PlaceAnOrderCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(1);
    }
}