using CRMD.Domain.Sections;

namespace CRMD.Application.Common.Interfaces
{
    public interface ISectionRepository
    {
        Task AddSectionAsync(Section section);
        Task DeleteSectionAsync(int sectionId);
        Task<SectionDto?> GetSectionByIdAsync(int sectionId);
        Task<List<SectionDto>?> GetSectionsAsync();
        Task UpdateSectionAsync(Section newSectionData);
    }
}