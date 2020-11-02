using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExamSystemBackend.Models
{
    public class Participant : ModelBase
    {
        public ParticipantType Type { get; set; }
        
        public string Name { get; set; }
        
        public List<Class> Classes { get; set; }
    }
}