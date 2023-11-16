using ExamSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Services.Contract
{
    public interface IExamService : IGenericService<Exam>
    {
        Task<IReadOnlyList<ExamOverView>> GetExamOverviewAsync(string userId, int examId);

        Task<Exam?> GetCurrentExamDetailsAsync(string userId);

        Task<IReadOnlyList<Exam>> GetAllExamDetailsForUserAsync(string userId);

        Task<Exam> UpdateCompleteExamAsync(string userId);
    }
}
