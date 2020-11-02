using System;
using System.Collections.Generic;

namespace ExamSystemBackend.Models
{
    public class Question : ModelBase
    {
        public string Title { get; set; }

        public List<string> PossibleAnswers { get; set; }
        
        public Participant Author { get; set; }
        
        public int TotalMark { get; set; }
    }
}