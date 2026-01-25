using CRMD.Domain.Attributes;

namespace CRMD.Domain.Tables
{
    public class Table
    {
        [IgnoreOn(enOperationMode.Add)]
        public int TableId { get; set; }
        [IgnoreOn(enOperationMode.Update)]
        public int SectionId { get; set; }
        public int WaiterId { get; set; }
        public short Capacity { get; set; }
    }
}