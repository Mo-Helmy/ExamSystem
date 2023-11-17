using ExamSystem.Application.Specifications.CertificateSpec;
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
        Task<Exam> CreateExamAsync(string userId, int certificateId);

        Task<Exam> GetCurrentExamDetailsAsync(string userId);

        Task<IReadOnlyList<ExamQuestionView>> GetCurrentExamQuestionsAsync(int examId);

        Task<IReadOnlyList<Exam>> GetAllExamsForUserAsync(string userId);

        Task<Exam> UpdateCompleteExamAsync(string userId);

        Task<IReadOnlyList<ExamReview>> GetExamReviews(string userId, int examId);

    }
}
