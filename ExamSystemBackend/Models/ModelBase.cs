using System;
using System.ComponentModel.DataAnnotations;

namespace ExamSystemBackend.Models
{
    public abstract class ModelBase
    {
        [Key] public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}