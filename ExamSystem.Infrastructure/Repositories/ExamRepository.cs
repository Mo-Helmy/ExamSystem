using ExamSystem.Domain.Entities;
using ExamSystem.Infrastructure.Data;
using ExamSystem.Infrastructure.Repositories.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Infrastructure.Repositories
{
    public class ExamRepository : GenericRepository<Exam>, IExamRepository
    {
        public ExamRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Exam?> CreateExamWithQuestions(string userId, int certificateId)
        {
            var result = await _dbContext.Exams
                .FromSql($"EXEC [dbo].[sp_CreateExamAndQuestions]  @UserId = {userId}, @CertificateId = {certificateId};")
                .IgnoreQueryFilters().ToListAsync();
            
            return result.FirstOrDefault();
        }

        public async Task<IReadOnlyList<ExamQuestionView>> GetExamQuestionsWithAnswers(int ExamId)
        {
            var result = await _dbContext.ExamQuestionsWithAnswers.Where(x => x.ExamId == ExamId).ToListAsync();

            var mappedResult = result.GroupBy(row => new { row.QuestionId })
                .Select(group => new ExamQuestionView
                {
                    QuestionId = group.Key.QuestionId,
                    QuestionBody = group.First().QuestionBody,
                    Answers = group.Select(answer => new ExamAnswersView
                    {
                        AnswerId = answer.AnswerId,
                        AnswerBody = answer.AnswerBody
                    }).ToList()
                }).ToList();

            return mappedResult;
        }

        public decimal? CalculateExamScore(int examId)
        {
            return (decimal?) _dbContext.Database.SqlQuery<double?>($"SELECT dbo.[fn_CalculateExamScore] ({examId});").AsEnumerable().FirstOrDefault(); ;
        }

        public async Task<IReadOnlyList<ExamReview>> GetExamReviews (int examId)
        {
            return await _dbContext.ExamReviews.Where(x => x.ExamId==examId).ToListAsync();
        }


    }
}
