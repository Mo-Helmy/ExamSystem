using ExamSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Dtos.QuestionDtos
{
    public class QuestionDto
    {
        public string QuestionBody { get; set; }

        public IEnumerable<AnswerDto> Answers { get; set; }
    }
}
