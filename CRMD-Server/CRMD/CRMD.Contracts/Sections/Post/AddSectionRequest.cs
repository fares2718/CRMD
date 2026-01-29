namespace CRMD.Contracts.Sections.Post;

public record AddSectionRequest(
    int CategoryId,
    decimal Price,
    string Name
);