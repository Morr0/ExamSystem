using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExamSystemBackend.Models
{
    public class Class : ModelBase
    {
        public string Name { get; set; }
        
        public List<Participant> Teachers { get; set; } = new List<Participant>();
        
        public List<Participant> Students { get; set; } = new List<Participant>();
        
        public ClassState State { get; set; }
        
        public List<ExamReport> ExamReports { get; set; } = new List<ExamReport>();
    }
}