using System.Collections.Generic;
using System.Threading.Tasks;
using ExamSystemBackend.Models;
using ExamSystemBackend.Services.ExamHandlingService.DTOs;

namespace ExamSystemBackend.Services.ExamHandlingService
{
    public interface IExamHandlingService
    {
        Task<ExamReport> CreateExam(string teacherId, Exam exam);
        Task SetExamReportState(TeacherModifyExamDTO dto);

        Task<Exam> GetExam(string participantId, string examId);
        Task RespondToQuestion(StudentQuestionResponseDTO dto);
        
        Task<ExamReport> GetExamReport(string teacherId, string classId, string examId);
        Task<List<ExamReport>> GetExamReport(string teacherId, string classId);

        Task<List<StudentExamResultDTO>> GetStudentExamScores();
    }
}