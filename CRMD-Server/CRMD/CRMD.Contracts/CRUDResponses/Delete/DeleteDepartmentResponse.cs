namespace CRMD.Contracts.CRUDResponses.Delete
{
    public record DeleteResponse(ErrorOr<Deleted> response);
}