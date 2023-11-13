using ExamSystem.Application.Dtos.QuestionDtos;
using ExamSystem.Domain.Entities;
using ExamSystem.Infrastructure.UnitOfWorks.Contract;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Dtos.CertificateDtos.Validators
{
    public class DeleteCertificateValidator : AbstractValidator<DeleteCertificateDto>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteCertificateValidator(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            ApplyValidation();
        }

        public void ApplyValidation()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .MustAsync(async (id, _) => await unitOfWork.Repository<Certificate>().GetByIdAsync(id) is not null)
                .WithMessage("Certificate Id is not exist!");
        }
    }
}
