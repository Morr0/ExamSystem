using System;
using System.Collections.Generic;

namespace ExamSystemBackend.Models
{
    public class Exam : ModelBase
    {
        public string Name { get; set; }

        public List<Question> Questions { get; set; }
    }
}