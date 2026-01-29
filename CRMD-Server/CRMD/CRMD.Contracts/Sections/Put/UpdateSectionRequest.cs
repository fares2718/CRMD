namespace CRMD.Contracts.Sections.Put;

public record UpdateSectionRequest(
    int SectionId,
    int CaptainId,
    short TablesCount
);