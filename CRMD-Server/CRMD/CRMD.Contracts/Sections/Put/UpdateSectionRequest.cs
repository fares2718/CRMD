namespace CRMD.Contracts.Sections.Put;

public record UpdateSectionRequest(
    int ItemId,
    decimal Price
);