using ExamSystem.Application.Services;
using ExamSystem.Application.Services.Contract;
using ExamSystem.Application.Specifications.Questions;
using ExamSystem.Domain.Entities;
using ExamSystem.Infrastructure.Specifications;
using ExamSystem.Infrastructure.UnitOfWorks.Contract;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Dtos.CertificateTopicDtos.Validators
{
    public class UpdateCertificateTopicValidator : AbstractValidator<UpdateCertificateTopicDto>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICertificateTopicService certificateTopicService;
        private readonly IQuestionService questionService;

        public UpdateCertificateTopicValidator(IUnitOfWork unitOfWork, ICertificateTopicService certificateTopicService, IQuestionService questionService)
        {
            this.unitOfWork = unitOfWork;
            this.certificateTopicService = certificateTopicService;
            this.questionService = questionService;
            ApplyValidation();
        }

        public void ApplyValidation()
        {
            RuleFor(x => x.CertificateId).MustAsync(async (certificateId, _) =>
            {
                return await unitOfWork.Repository<Certificate>().GetByIdAsync(certificateId) is not null;
            }).WithMessage("Certificate Id is not exist!");

            RuleFor(x => x.TopicId).MustAsync(async (topicId, _) =>
            {
                return await unitOfWork.Repository<Topic>().GetByIdAsync(topicId) is not null;
            }).WithMessage("Topic Id is not exist!");


            RuleFor(x => x).MustAsync(async (certificateTopic, _) =>
            {
                var availableQuestionsCount = await unitOfWork.Repository<Question>()
                 .GetCountWithSpecAsync(new BaseSpecification<Question>(x => x.TopicId == certificateTopic.TopicId));

                return availableQuestionsCount >= certificateTopic.QuestionCount;
            }).WithMessage(errorMessage: $"QuestionCount is more that the available questions in this topic");
            //}).WithMessage(errorMessage: $"The Available Question Count in your selected topic is {availableQuestionsCount}");


            RuleFor(x => x.QuestionCount).GreaterThanOrEqualTo(1);

            RuleFor(x => x).MustAsync(async (x, _) =>
            {
                var currentTotalPercentage = (await certificateTopicService.GetAllAsync(x.CertificateId)).Sum(x => x.TopicPercentage);

                var currentTopicPercentage = (await unitOfWork.Repository<CertificateTopic>().GetByIdAsync(x.Id)).TopicPercentage;

                var updatedTotalPercentage = currentTotalPercentage - currentTopicPercentage;

                return updatedTotalPercentage + x.TopicPercentage <= 1;

                //return (await certificateTopicService.GetAllAsync(x.CertificateId)).Sum(x => x.TopicPercentage) + x.TopicPercentage <= 1;
            }).WithMessage("The sum of TopicPercentage for all certificate topics must be not more than 1");

        }
    }
}
