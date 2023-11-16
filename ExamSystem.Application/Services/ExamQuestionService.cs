using ExamSystem.Application.Dtos.ExamQuestion;
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
    public class ExamQuestionService : GenericService<ExamQuestion>, IExamQuestionService
    {
        public ExamQuestionService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<ExamQuestion> UpdateAnswerInExamQuestionAsync(UpdateAnswerIdInExamQuestionDto updateAnswerIdDto)
        {
            var currentExamQuestion = await _unitOfWork.Repository<ExamQuestion>()
                .GetByIdWithSpecAsync(new BaseSpecification<ExamQuestion>(x => x.ExamId == updateAnswerIdDto.ExamId && x.QuestionId == updateAnswerIdDto.QuestionId));

            if (currentExamQuestion is null) throw new KeyNotFoundException("ExamQuestion Not Found");

            currentExamQuestion.AnswerId = updateAnswerIdDto.AnswerId;
            currentExamQuestion.UpdatedAt = DateTime.Now;

            _unitOfWork.Repository<ExamQuestion>().Update(currentExamQuestion);

            var affectedRows = await _unitOfWork.CompleteAsync();

            if (affectedRows == 0) throw new InvalidOperationException("Update ExamQuestion Failed!");

            return currentExamQuestion;
        }

    }
}
