using ExamSystem.Application.Dtos.QuestionDtos;
using ExamSystem.Domain.Entities;
using ExamSystem.Infrastructure.UnitOfWorks.Contract;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Dtos.QuestionDtos.Validators
{
    public class UpdateQuestionValidator : AbstractValidator<UpdateQuestionDto>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateQuestionValidator(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            ApplyValidation();
        }

        public async void ApplyValidation()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .MustAsync(async (id, _) => await unitOfWork.Repository<Question>().GetByIdAsync(id) is not null)
                .WithMessage("Question Id is not exist!");

            RuleFor(x => x.Answers)
                .NotEmpty()
                .WithMessage("Answers Count should be more than one!");

            RuleFor(x => x.Answers)
                .Must(x => x.Count() <= 5)
                .WithMessage("Answers Count should be less than or equal 5!");

            RuleFor(x => x.Answers.Count(x => x.IsRight == true))
                .Equal(1)
                .WithMessage("True Answers Count should be one!");

            RuleFor(x => x.TopicId)
                .MustAsync(async (topicId, _) =>
                {
                    return await unitOfWork.Repository<Topic>().GetByIdAsync(topicId) is not null;
                })
                .WithMessage("Topic Id Not Found!");
        }
    }
}
