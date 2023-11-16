using ExamSystem.Application.Dtos.QuestionDtos;
using ExamSystem.Application.Specifications.Questions;
using ExamSystem.Application.Specifications.TopicSpec;
using ExamSystem.Domain.Entities;
using ExamSystem.Infrastructure.UnitOfWorks.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Services
{
    public interface IQuestionService 
    {
        Task<IReadOnlyList<Question>> GetAllPaginatedAsync(QuestionSpecificationParams specificationParams);

        Task<int> GetAllCountPaginatedAsync(QuestionSpecificationParams specificationParams);

        Task<Question?> GetByIdAsync(int questionId);

        Task<Question?> CreateAsync(Question question);

        Task<Question> UpdateAsync(UpdateQuestionDto updateQuestionDto);

        Task DeleteAsync(int questionId);

        //Task<IReadOnlyList<Question>> GetExamQuestions(int examId);
    }
}
