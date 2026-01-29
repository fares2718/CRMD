namespace CRMD.Contracts.Tables.Post;

public record AddTablRequest(
    int SectionId,
        int WaiterId,
        short Capacity
);