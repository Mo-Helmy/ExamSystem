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

    }
}
