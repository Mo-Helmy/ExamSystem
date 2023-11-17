using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Domain.Entities
{
    public class ExamQuestionsWithAnswers
    {
        public int ExamId { get; set; }
        public int QuestionId { get; set; }
        public string QuestionBody { get; set; }
        public int AnswerId { get; set; }
        public string AnswerBody { get; set; }
    }

    public class ExamQuestionView
    {
        public int QuestionId { get; set; }
        public string QuestionBody { get; set; }
        public IEnumerable<ExamAnswersView> Answers { get; set; }
    }

    public class ExamAnswersView
    {
        public int AnswerId { get; set; }
        public string AnswerBody { get; set; }
    }

    public class ExamReview
    {
        public int ExamId { get; set; }
        public int QuestionId { get; set; }
        public string QuestionBody { get; set; }
        public int? AnswerId { get; set; }
        public string? AnswerBody { get; set; }
        public bool? IsRight { get; set; }
    }
}
