using System;
using System.ComponentModel.DataAnnotations;

namespace ExamSystemBackend.Models
{
    public abstract class ModelBase
    {
        [Key]
        public Guid Id { get; set; }
    }
}