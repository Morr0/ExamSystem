using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamSystemBackend.Models
{
    public class QuestionResponse : ModelBase
    {
        [ForeignKey(nameof(QuestionId))]
        public string QuestionId { get; set; }
        public Question Question { get; set; }

        public string Response { get; set; }

        [ForeignKey(nameof(StudentId))]
        public string StudentId { get; set; }
        public Participant Student { get; set; }
        
        public int ScoredMark { get; set; }
    }
}