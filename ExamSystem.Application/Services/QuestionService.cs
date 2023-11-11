using ExamSystem.Application.Specifications.Questions;
using ExamSystem.Domain.Entities;
using ExamSystem.Infrastructure.Specifications;
using ExamSystem.Infrastructure.UnitOfWorks.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork unitOfWork;

        public QuestionService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<Question>> GetAllPaginatedAsync(QuestionSpecificationParams specificationParams)
        {
            var questionSpecification = new QuestionSpecification(specificationParams);

            return await unitOfWork.Repository<Question>().GetAllWithSpecAsync(questionSpecification);
        }


        //public async Task<IReadOnlyList<Question>> GetByIdAsync(QuestionSpecificationParams specificationParams)
        //{

        //// Get By Id Specification
        //    var qSpec = new BaseSpecification<Question>(x => x.Id == 5 && x.QuestionBody == "dd")
        //    {
        //        Includes = new List<System.Linq.Expressions.Expression<Func<Question, object>>>() { x => x.Answers }
        //    };

        //    return await unitOfWork.Repository<Question>()(questionSpecification);
        //}

        public async Task<int> GetAllCountPaginatedAsync(QuestionSpecificationParams specificationParams)
        {
            var questionSpecification = new QuestionCountSpecification(specificationParams);

            return await unitOfWork.Repository<Question>().GetCountWithSpecAsync(questionSpecification);
        }

    }
}
