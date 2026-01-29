namespace CRMD.Application.Generics.Queries
{
    public record GetByIdQuery<T>(int Id) : IRequest<ErrorOr<T>>;
}
