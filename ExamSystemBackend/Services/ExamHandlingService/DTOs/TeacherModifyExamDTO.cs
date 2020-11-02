using ExamSystemBackend.Models;

namespace ExamSystemBackend.Services.ExamHandlingService.DTOs
{
    public class TeacherModifyExamDTO
    {
        public string TeacherId { get; set; }

        public string ExamId { get; set; }

        public ExamReportStatus NewExamReportStatus { get; set; }
    }
}