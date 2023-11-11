using ExamSystem.Domain.Entities;
using ExamSystem.Infrastructure.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Specifications.Questions
{
    public class QuestionCountSpecification : BaseSpecification<Question>
    {
        public QuestionCountSpecification(QuestionSpecificationParams specificationParams)
        {
            if (specificationParams.TopicId is not null) CriteriaList.Add(x => x.TopicId == specificationParams.TopicId);

            if (specificationParams.Search is not null) CriteriaList.Add(x => x.QuestionBody.ToLower().Contains(specificationParams.Search) ||
                                                                               x.Answers.Any(x => x.AnswerBody.ToLower().Contains(specificationParams.Search)));
        }
    }
}
