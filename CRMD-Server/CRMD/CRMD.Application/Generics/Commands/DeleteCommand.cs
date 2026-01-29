namespace CRMD.Application.Generics.Commands
{
    public record DeleteCommand<T>(int Id) : IRequest<ErrorOr<Deleted>>;
}