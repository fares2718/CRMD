namespace CRMD.Contracts.CRUDResponses.Post
{
    public record AddResponse(ErrorOr<Created> response);
    public record sucAddResponse(ErrorOr<Success> response);
}