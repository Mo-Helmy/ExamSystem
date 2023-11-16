using ExamSystem.Application.Services.Contract;
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
    public class ExamService : GenericService<Exam>, IExamService
    {
        public ExamService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<IReadOnlyList<Exam>> GetAllExamDetailsForUserAsync(string userId)
        {
            var examSpec = new BaseSpecification<Exam>(x => x.UserId == userId)
            {
                Includes = new() { x => x.Certificate }
            };

            return await _unitOfWork.Repository<Exam>().GetAllWithSpecAsync(examSpec);
        }

        public async Task<Exam?> GetCurrentExamDetailsAsync(string userId)
        {
            var examSpec = new BaseSpecification<Exam>(x => x.UserId == userId && x.ExamEndTime > DateTime.Now)
            {
                Includes = new() { x => x.Certificate }
            };

            return await _unitOfWork.Repository<Exam>().GetByIdWithSpecAsync(examSpec);
        }

        public async Task<IReadOnlyList<ExamOverView>> GetExamOverviewAsync (string userId, int examId)
        {
            var examOverview = await _unitOfWork.ExamRepository.GetExamOverView(examId) 
                ?? throw new KeyNotFoundException("Exam Id not Found");

            // to check if this exam id belongs to the current user.
            var currentExam = await _unitOfWork.ExamRepository
                .GetByIdWithSpecAsync(new BaseSpecification<Exam>(x => x.Id == examId && x.UserId == userId)) 
                ?? throw new UnauthorizedAccessException("you are trying to access protected resources");

            return examOverview;
        }

        public async Task<Exam> UpdateCompleteExamAsync(string userId)
        {
            //var currentExam = await _unitOfWork.ExamRepository
            //    .GetByIdWithSpecAsync(new BaseSpecification<Exam>(x => x.Id == examId && x.UserId == userId))
            //    ?? throw new KeyNotFoundException("Exam Id not Found");

            var currentExam = (await GetAllExamDetailsForUserAsync(userId)).LastOrDefault()
                ?? throw new KeyNotFoundException("Exam Id not Found");

            if(DateTime.Now >  currentExam.ExamEndTime.AddMinutes(10))
                throw new InvalidOperationException("Exam ended you can't submit it after 10 minutes from ExamEndTime!");

            // if exam ended you cant update it again
            if (currentExam.ExamResult is not null)
                throw new InvalidOperationException("Exam ended you cant update it again!");

            currentExam.UpdatedAt = DateTime.Now;

            if(currentExam.ExamEndTime > DateTime.Now)
                currentExam.ExamCompletedTime = DateTime.Now;

            var examResult = _unitOfWork.ExamRepository.GetTotalSuccessPercentage(currentExam.Id);
            currentExam.ExamResult = (decimal) examResult;

            var certificate = await _unitOfWork.Repository<Certificate>()
                .GetByIdWithSpecAsync(new BaseSpecification<Certificate>(x => x.Id == currentExam.CertificateId));

            var isPassed = examResult > certificate!.PassScore;
            currentExam.IsPassed = isPassed;

            _unitOfWork.ExamRepository.Update(currentExam);

            var rowsAffected = await _unitOfWork.CompleteAsync();

            if (rowsAffected == 0) throw new InvalidOperationException("update exam failed");

            return currentExam;
        }

    }
}
