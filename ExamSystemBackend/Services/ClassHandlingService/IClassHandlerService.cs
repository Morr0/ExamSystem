using System.Collections.Generic;
using System.Threading.Tasks;
using ExamSystemBackend.Models;

namespace ExamSystemBackend.Services.ClassHandlingService
{
    public interface IClassHandlerService
    {
        Task AddParticipant(Participant participant, string classId);
        Task<Participant> GetParticipant(string id);
        Task<List<Participant>> GetParticipants(string classId);
        
        Task AddExamReport(ExamReport examReport);
        Task<ExamReport> GetExamReport(string participantId, string examReportId);
        Task<List<ExamReport>> GetExamReports();
    }
}