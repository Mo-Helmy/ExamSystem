using ExamSystem.Domain.Entities;
using ExamSystem.Infrastructure.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Specifications.Questions
{
    public class QuestionSpecification : BaseSpecification<Question>
    {
        public QuestionSpecification(QuestionSpecificationParams specificationParams)
        {
            Includes.Add(x => x.Answers);

            if (specificationParams.TopicId is not null) CriteriaList.Add(x => x.TopicId == specificationParams.TopicId);

            if (specificationParams.Search is not null) CriteriaList.Add(x => x.QuestionBody.ToLower().Contains(specificationParams.Search) ||
                                                                               x.Answers.Any(x => x.AnswerBody.ToLower().Contains(specificationParams.Search)));

            ApplyPagination(specificationParams.PageSize * (specificationParams.PageIndex - 1), specificationParams.PageSize);


            if(specificationParams.Sort is not null)
            {
                switch (specificationParams.Sort)
                {
                    case "IdAsc":
                        AddOrderBy(x => x.Id);
                        //OrderBy = x => x.Id;
                        break;
                    case "IdDesc":
                        AddOrderByDescending(x => x.Id);
                        //OrderByDescending = x => x.Id;
                        break;
                    default:
                        AddOrderBy(x => x.QuestionBody);
                        break;
                }
            }
        }
    }
}
