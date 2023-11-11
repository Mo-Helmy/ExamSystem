﻿using ExamSystem.Application.Specifications.Questions;
using ExamSystem.Domain.Entities;
using ExamSystem.Infrastructure.UnitOfWorks.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Services
{
    public interface IQuestionService 
    {
        Task<IReadOnlyList<Question>> GetAllPaginatedAsync(QuestionSpecificationParams specificationParams);

        Task<int> GetAllCountPaginatedAsync(QuestionSpecificationParams specificationParams);


    }
}