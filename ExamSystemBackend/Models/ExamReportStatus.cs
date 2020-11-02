namespace ExamSystemBackend.Models
{
    public enum ExamReportStatus : byte
    {
        Invalid = 0,
        NotStarted,
        Running,
        Finished,
        Marked
    }
}