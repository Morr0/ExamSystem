using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamSystemBackend.Models
{
    public class Exam : ModelBase
    {
        public string Name { get; set; }

        public List<Question> Questions { get; set; } = new List<Question>();
        
        [ForeignKey(nameof(AuthorId))]
        public string AuthorId { get; set; }
        public Participant Author { get; set; }
    }
}