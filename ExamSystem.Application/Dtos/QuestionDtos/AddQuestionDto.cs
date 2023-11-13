using ExamSystem.Application.Dtos.AnswersDtos;
using ExamSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Dtos.QuestionDtos
{
    public class AddQuestionDto
    {
        public string QuestionBody { get; set; }
        public int TopicId { get; set; }

        public IEnumerable<AddAnswerDto> Answers { get; set; }
    }
}
