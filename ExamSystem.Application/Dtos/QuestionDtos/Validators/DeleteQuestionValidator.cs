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
    public class DeleteQuestionValidator : AbstractValidator<DeleteQuestionDto>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteQuestionValidator(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            ApplyValidation();
        }

        public void ApplyValidation()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .MustAsync(async (id, _) => await unitOfWork.Repository<Question>().GetByIdAsync(id) is not null)
                .WithMessage("Question Id is not exist!");
        }
    }
}
