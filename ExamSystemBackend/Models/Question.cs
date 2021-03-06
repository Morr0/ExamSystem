﻿using System;
using System.Collections.Generic;

namespace ExamSystemBackend.Models
{
    public class Question : ModelBase
    {
        public string Title { get; set; }

        public List<string> Choices { get; set; } = new List<string>();
        public string CorrectAnswer { get; set; }

        public int TotalMark { get; set; }
    }
}