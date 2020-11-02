using System.Collections.Generic;
using System.Threading.Tasks;
using ExamSystemBackend.Models;
using ExamSystemBackend.Services.ExamHandlingService.DTOs;

namespace ExamSystemBackend.Services.ClassHandlingService
{
    public interface IClassHandlerService
    {
        Task<Class> AddClass(string teacherId, Class @class);
        Task<Class> GetClass(string classId);
        Task<Class> ModifyClassState(string teacherId, string classId, ClassState state);
            
        Task<Participant> AddParticipant(Participant participant, string classId);
        Task<List<Participant>> GetParticipants(string classId);
        
        Task<ExamReport> PutExam(string examId);

        Task<QuestionResponse> RespondToQuestion(StudentQuestionResponseDTO dto);
        Task<int> GetExamScore(string studentId, string examReportId);
    }
}