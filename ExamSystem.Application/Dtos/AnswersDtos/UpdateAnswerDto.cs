﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Dtos.AnswersDtos
{
    public class UpdateAnswerDto
    {
        public int Id { get; set; }
        public string AnswerBody { get; set; }
        public bool IsRight { get; set; }
    }
}
