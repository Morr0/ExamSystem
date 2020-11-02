namespace ExamSystemBackend.Services.ExamHandlingService.DTOs
{
    public class StudentExamResultDTO
    {
        public string ClassId { get; set; }
        
        public string ExamId { get; set; }
        
        public int ExamScore { get; set; }
    }
}