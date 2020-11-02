using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamSystemBackend.Models
{
    public class QuestionResponse : ModelBase
    {
        public Question Question { get; set; }

        public string Response { get; set; }

        public Participant Student { get; set; }
        
        public int ScoredMark { get; set; }
    }
}