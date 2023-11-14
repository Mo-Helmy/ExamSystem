using ExamSystem.Domain.Entities;
using ExamSystem.Infrastructure.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Specifications.TopicSpec
{
    public class TopicSpecification : BaseSpecification<Topic>
    {
        public TopicSpecification(TopicSpecificationParams specificationParams)
        {
            Includes.Add(x => x.Category);

            if(specificationParams.CategoryId != null) CriteriaList.Add(x => x.CategoryId == specificationParams.CategoryId);

            if(specificationParams.Search is not null) CriteriaList.Add(x => 
            x.TopicName.ToLower().Contains(specificationParams.Search) || x.TopicDescription.ToLower().Contains(specificationParams.Search)
            );

            if(specificationParams.Sort is not null)
            {
                switch (specificationParams.Sort)
                {
                    case "NameAsc":
                        AddOrderBy(x => x.TopicName);
                        //OrderBy = x => x.Id;
                        break;
                    case "NameDesc":
                        AddOrderByDescending(x => x.TopicName);
                        //OrderByDescending = x => x.Id;
                        break;
                    case "DescriptionAsc":
                        AddOrderBy(x => x.TopicDescription);
                        //OrderBy = x => x.Id;
                        break;
                    case "DescriptionDesc":
                        AddOrderByDescending(x => x.TopicDescription);
                        //OrderByDescending = x => x.Id;
                        break;
                    default:
                        AddOrderBy(x => x.Id);
                        break;
                }
            }
        }

        //public TopicSpecification(int questionId) : base(x => x.Id == questionId)
        //{
        //    Includes.Add(x => x.Answers);
        //}

    }
}
