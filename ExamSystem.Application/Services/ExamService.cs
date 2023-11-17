using ExamSystem.Application.Services.Contract;
using ExamSystem.Domain.Entities;
using ExamSystem.Infrastructure.UnitOfWorks.Contract;
using ExamSystem.Application.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ExamSystem.Infrastructure.Specifications;
using ExamSystem.Application.Helper;

namespace ExamSystem.Application.Services
{
    public class ExamService : GenericService<Exam>, IExamService
    {
        private readonly ICertificateService _certificateService;
        private readonly IQuestionService _questionService;

        public ExamService(IUnitOfWork unitOfWork, ICertificateService certificateService, IQuestionService questionService) : base(unitOfWork)
        {
            this._certificateService = certificateService;
            this._questionService = questionService;
        }

        public async Task<Exam> CreateExamAsync(string userId, int certificateId)
        {
            var currentActiveExam = await _unitOfWork.Repository<Exam>()
                .GetByIdWithSpecAsync(new BaseSpecification<Exam>(x => x.UserId == userId && x.ExamEndTime > DateTime.Now));

            if (currentActiveExam is not null)
                return currentActiveExam;

            var createdExam = await _unitOfWork.ExamRepository.CreateExamWithQuestions(userId, certificateId)
                ?? throw new InvalidOperationException("Create new exam failed!");

            return createdExam;
        }

        public async Task<Exam> GetCurrentExamDetailsAsync(string userId)
        {
            var examSpec = new BaseSpecification<Exam>(x => x.UserId == userId && x.ExamEndTime > DateTime.Now)
            {
                Includes = new() { x => x.Certificate }
            };

            var exam = await _unitOfWork.Repository<Exam>().GetByIdWithSpecAsync(examSpec)
                ?? throw new KeyNotFoundException("No Currently Active Exams for you!");

            return exam;
        }


        public async Task<IReadOnlyList<ExamQuestionView>> GetCurrentExamQuestionsAsync(int examId)
        {

            return await _unitOfWork.ExamRepository.GetExamQuestionsWithAnswers(examId);
        }

        public async Task<Exam> UpdateCompleteExamAsync(string userId)
        {
            //var currentExam = await _unitOfWork.ExamRepository
            //    .GetByIdWithSpecAsync(new BaseSpecification<Exam>(x => x.Id == examId && x.UserId == userId))
            //    ?? throw new KeyNotFoundException("Exam Id not Found");

            var currentExam = (await GetAllExamsForUserAsync(userId)).LastOrDefault()
                ?? throw new KeyNotFoundException("Exam Id not Found");

            if (DateTime.Now > currentExam.ExamEndTime.AddMinutes(50))
                throw new InvalidOperationException("Exam ended you can't submit it after 5 minutes from ExamEndTime!");

            // if exam ended you cant update it again
            if (currentExam.ExamScore is not null)
                throw new InvalidOperationException("Exam ended you cant update it again!");

            currentExam.UpdatedAt = DateTime.Now;

            if (currentExam.ExamEndTime > DateTime.Now)
                currentExam.ExamCompletedTime = DateTime.Now;

            var examResult = _unitOfWork.ExamRepository.CalculateExamScore(currentExam.Id);
            currentExam.ExamScore = examResult ?? 0;

            var certificate = await _unitOfWork.Repository<Certificate>()
                .GetByIdWithSpecAsync(new BaseSpecification<Certificate>(x => x.Id == currentExam.CertificateId));

            var isPassed = examResult > certificate?.PassScore;
            currentExam.IsPassed = isPassed;

            _unitOfWork.ExamRepository.Update(currentExam);

            var rowsAffected = await _unitOfWork.CompleteAsync();

            if (rowsAffected == 0) throw new InvalidOperationException("update exam failed");

            return currentExam;
        }


        public async Task<IReadOnlyList<Exam>> GetAllExamsForUserAsync(string userId)
        {
            var examSpec = new BaseSpecification<Exam>(x => x.UserId == userId)
            {
                Includes = new() { x => x.Certificate }
            };

            return await _unitOfWork.Repository<Exam>().GetAllWithSpecAsync(examSpec);
        }

        public async Task<IReadOnlyList<ExamReview>> GetExamReviews(string userId, int examId)
        {
            var exam = await _unitOfWork.ExamRepository.GetByIdAsync(examId) 
                ?? throw new KeyNotFoundException("exam id is not exist");
            if (exam.UserId != userId)
                throw new UnauthorizedAccessException("you are unauthorized to view this recourses");

            return await _unitOfWork.ExamRepository.GetExamReviews(examId);
        }

    }
}
