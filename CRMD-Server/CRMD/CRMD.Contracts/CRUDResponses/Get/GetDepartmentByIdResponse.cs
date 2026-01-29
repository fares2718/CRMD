namespace CRMD.Contracts.CRUDResponses.Get
{
    public record GetByIdResponse<T>(ErrorOr<T> response);
}