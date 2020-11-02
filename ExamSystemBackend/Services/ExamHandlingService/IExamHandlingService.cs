using System.Collections.Generic;
using System.Threading.Tasks;
using ExamSystemBackend.Models;
using ExamSystemBackend.Services.ExamHandlingService.DTOs;

namespace ExamSystemBackend.Services.ExamHandlingService
{
    public interface IExamHandlingService
    {
        Task<Exam> AddExam(Exam exam);
        Task<Exam> GetExam(string participantId, string examId);
    }
}