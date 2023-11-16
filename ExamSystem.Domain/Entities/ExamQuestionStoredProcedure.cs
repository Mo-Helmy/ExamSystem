using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Domain.Entities
{
    public class ExamQuestionStoredProcedure
    {
        public int QuestionId { get; set; }
        public string QuestionBody { get; set; }
        public IEnumerable<ExamAnswersStoredProcedure> Answers { get; set; }
    }

    public class ExamAnswersStoredProcedure    
    {
        public int AnswerId { get; set; }
        public string AnswerBody { get; set; }
    }

    [NotMapped]
    public class ExamQuestionWithAnswerStoredProcedure
    {
        public int ExamId { get; set; }
        public int QuestionId { get; set; }
        public string QuestionBody { get; set; }
        public int AnswerId { get; set; }
        public string AnswerBody { get; set; }
    }

    public class ExamQuestionWithAnswerView
    {
        public int ExamId { get; set; }
        public int QuestionId { get; set; }
        public string QuestionBody { get; set; }
        public int AnswerId { get; set; }
        public string AnswerBody { get; set; }
    }

    [NotMapped]
    public class ExamOverView
    {
        public int QuestionId { get; set; }
        public string QuestionBody { get; set; }
        public string? AnswerBody { get; set; }
        public bool? IsRight { get; set; }
    }


}
