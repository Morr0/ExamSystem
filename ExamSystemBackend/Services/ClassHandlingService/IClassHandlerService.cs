using System.Collections.Generic;
using System.Threading.Tasks;
using ExamSystemBackend.Models;

namespace ExamSystemBackend.Services.ClassHandlingService
{
    public interface IClassHandlerService
    {
        Task<Class> AddClass(string teacherId, Class @class);
        Task<Class> GetClass(string classId);
        Task<Class> ModifyClassState(string teacherId, string classId, ClassState state);
            
        Task<Participant> AddParticipant(Participant participant, string classId);
        Task<List<Participant>> GetParticipants(string classId);
        
        Task AddExamReport(ExamReport examReport);
        Task<ExamReport> GetExamReport(string participantId, string examReportId);
        Task<List<ExamReport>> GetExamReports();
    }
}