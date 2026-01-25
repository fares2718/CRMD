using CRMD.Domain.Attributes;

namespace CRMD.Domain.Sections
{
    public class Section
    {
        [IgnoreOn(enOperationMode.Add)]
        public int SectionId { get; set; }
        public int CaptainId { get; set; }
        public short TablesCount { get; set; }
    }
}