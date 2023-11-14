using ExamSystem.Domain.Entities;
using ExamSystem.Infrastructure.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Specifications.Questions
{
    public class ExamQuestionSpecification : BaseSpecification<Question>
    {
        public ExamQuestionSpecification(List<int> questionIds) : base(x => questionIds.Contains(x.Id))
        {
            Includes.Add(x => x.Answers);
        }
    }
}
