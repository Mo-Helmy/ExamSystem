using ExamSystem.Application.Dtos.AnswersDtos;
using ExamSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Dtos.QuestionDtos
{
    public class QuestionResponseDto
    {
        public int Id { get; set; }
        public string QuestionBody { get; set; }

        public IEnumerable<AnswerResponseDto> Answers { get; set; }
    }
}
