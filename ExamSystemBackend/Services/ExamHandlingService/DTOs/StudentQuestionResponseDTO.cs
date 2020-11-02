namespace ExamSystemBackend.Services.ExamHandlingService.DTOs
{
    public class StudentQuestionResponseDTO
    {
        public string StudentId { get; set; }
        public string ExamId { get; set; }
        public string QuestionId { get; set; }
        public string Response { get; set; }
    }
}