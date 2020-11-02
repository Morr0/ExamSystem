using System.Collections.Generic;
using System.Threading.Tasks;
using ExamSystemBackend.Models;
using ExamSystemBackend.Services.ExamHandlingService.DTOs;

namespace ExamSystemBackend.Services.ExamHandlingService
{
    public class ExamHandlerService : IExamHandlingService
    {
        public async Task<Exam> AddExam(Exam exam)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Exam> GetExam(string participantId, string examId)
        {
            throw new System.NotImplementedException();
        }
    }
}