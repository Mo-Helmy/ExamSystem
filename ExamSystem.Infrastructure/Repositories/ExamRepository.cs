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

        public async Task<int> CreateExamAsync(string userId, int certificateId)
        {
            var result = _dbContext.Database.SqlQuery<int>($"EXEC dbo.CreateExamAndQuestions {userId}, {certificateId}, {null};")
                .AsEnumerable().ToList().FirstOrDefault();
            return result;

            //.ExamQuestionWithAnswerStoredProcedures
            //.FromSql($"EXEC CreateExamAndQuestions @UserId = {userId}, @CertificateId = {certificateId};").ToListAsync();

            //var selectedResult = result.GroupBy(row => new { row.QuestionId })
            //    .Select(group => new ExamQuestionStoredProcedure
            //    {
            //        QuestionId = group.Key.QuestionId,
            //        QuestionBody = group.First().QuestionBody,
            //        Answers = group.Select(answer => new ExamAnswersStoredProcedure
            //        {
            //            AnswerId = answer.AnswerId,
            //            AnswerBody = answer.AnswerBody
            //        }).ToList()
            //    }).ToList();


            //return selectedResult;
        }

        public async Task<IReadOnlyList<ExamQuestionStoredProcedure>> GetExamQuestion(int ExamId)
        {
            var result = await _dbContext.ExamQuestionWithAnswerViews.Where(x => x.ExamId == ExamId).ToListAsync();

            var mappedResult = result.GroupBy(row => new { row.QuestionId })
                .Select(group => new ExamQuestionStoredProcedure
                {
                    QuestionId = group.Key.QuestionId,
                    QuestionBody = group.First().QuestionBody,
                    Answers = group.Select(answer => new ExamAnswersStoredProcedure
                    {
                        AnswerId = answer.AnswerId,
                        AnswerBody = answer.AnswerBody
                    }).ToList()
                }).ToList();

            return mappedResult;
        }

        public async Task<IReadOnlyList<ExamOverView>> GetExamOverView(int examId)
        {
            return await _dbContext.ExamOverViews.FromSql($"EXEC dbo.ExamReview {examId};").ToListAsync();
        }

        public int GetTotalSuccessPercentage(int examId)
        {
            return AppDbContext.GetTotalSuccessPercentage(examId);
        }
    }
}
