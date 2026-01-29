namespace CRMD.Contracts.Sections.Post;

public record AddSectionRequest(
    int CaptainId,
    short TablesCount
);