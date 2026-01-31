using CRMD.Domain.Sections;
using CRMD.Infrastructure.Generics;

namespace CRMD.Infrastructure.Sections.Persistence
{
    public class SectionRepository : ISectionRepository
    {
        private readonly string _connectionString;

        public SectionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddSectionAsync(Section section)
        {
            await GenericRepository<Section>.AddAsync(section, _connectionString, "restocafe.addsection");
        }

        public async Task DeleteSectionAsync(int sectionId)
        {
            await GenericRepository<Section>.DeleteAsync(sectionId, _connectionString, "restocafe.deletesection");
        }

        public async Task<SectionDto?> GetSectionByIdAsync(int sectionId)
        {
            using (var reader = await GenericRepository<Section>
            .GetByIdAsync(sectionId, _connectionString, "restocafe.getsectionbyid(@id)"))
            {
                SectionDto section = new SectionDto();
                if (reader == null || !reader.HasRows)
                    return null;
                section = Mapper.Map<SectionDto>(reader);
                NpgsqlConnection.ClearAllPools();
                return section;
            }
        }

        public async Task<List<SectionDto>?> GetSectionsAsync()
        {
            var sections = new List<SectionDto>();
            using (var reader = await GenericRepository<SectionDto>
            .GetAllAsync(_connectionString, "restocafe.getsections()"))
            {
                if (reader == null || !reader.HasRows)
                    return null;
                while (await reader.ReadAsync())
                {
                    var section = Mapper.Map<SectionDto>(reader);
                    sections.Add(section);
                }
            }
            return sections.OrderBy(s => s.SectionId).ToList();
        }

        public async Task UpdateSectionAsync(Section newSectionData)
        {
            await GenericRepository<Section>.UpdateAsync(newSectionData, _connectionString, "restocafe.updatesection");
        }
    }
}