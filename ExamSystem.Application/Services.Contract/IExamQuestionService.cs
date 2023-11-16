using ExamSystem.Application.Dtos.ExamQuestion;
using ExamSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Services.Contract
{
    public interface IExamQuestionService : IGenericService<ExamQuestion>
    {
        Task<ExamQuestion> UpdateAnswerInExamQuestionAsync(UpdateAnswerIdInExamQuestionDto updateAnswerIdDto);
    }
}
