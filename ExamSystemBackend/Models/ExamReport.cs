using System;
using System.Collections.Generic;

namespace ExamSystemBackend.Models
{
    public class ExamReport : ModelBase
    {
        public Exam Exam { get; set; }

        public List<QuestionResponse> Responses { get; set; } = new List<QuestionResponse>();

        public ExamReportStatus Status { get; set; }
    }
}