namespace CRMD.Application.Generics.Queries
{
    public record GetAllQuery<T>() : IRequest<ErrorOr<List<T>>>;
}