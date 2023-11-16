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
        Task<int> CreateExamAsync(string userId, int certificateId);

        Task<IReadOnlyList<ExamQuestionStoredProcedure>> GetExamQuestion(int ExamId);

        Task<IReadOnlyList<ExamOverView>> GetExamOverView(int examId);

        int GetTotalSuccessPercentage(int examId);
    }
}
