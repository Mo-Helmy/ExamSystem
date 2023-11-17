using ExamSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Infrastructure.Repositories.Contract
{
    public interface IExamRepository : IGenericRepository<Exam>
    {
        Task<Exam?> CreateExamWithQuestions(string userId, int certificateId);

        Task<IReadOnlyList<ExamQuestionView>> GetExamQuestionsWithAnswers(int ExamId);

        decimal? CalculateExamScore(int examId);

        Task<IReadOnlyList<ExamReview>> GetExamReviews(int examId);
    }
}
