namespace CRMD.Contracts.CRUDResponses.Get
{
    public record GetAllResponse<T>(ErrorOr<List<T>> response);
}