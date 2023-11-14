using ExamSystem.Application.Dtos.QuestionDtos;
using ExamSystem.Application.Helper;
using ExamSystem.Application.Specifications.Questions;
using ExamSystem.Application.Specifications.TopicSpec;
using ExamSystem.Domain.Entities;
using ExamSystem.Infrastructure.Specifications;
using ExamSystem.Infrastructure.UnitOfWorks.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork unitOfWork;

        public QuestionService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<Question>> GetAllPaginatedAsync(QuestionSpecificationParams specificationParams)
        {
            var questionSpecification = new QuestionSpecification(specificationParams);

            return await unitOfWork.Repository<Question>().GetAllWithSpecAsync(questionSpecification);
        }

        public async Task<int> GetAllCountPaginatedAsync(QuestionSpecificationParams specificationParams)
        {
            var questionSpecification = new QuestionCountSpecification(specificationParams);

            return await unitOfWork.Repository<Question>().GetCountWithSpecAsync(questionSpecification);
        }

        public async Task<Question?> GetByIdAsync(int questionId)
        {
            // Get By Id Specification using the second constructor in QuestionSpecification class.
            var questionSpec = new QuestionSpecification(questionId);

            return await unitOfWork.Repository<Question>().GetByIdWithSpecAsync(questionSpec);
        }

        public async Task<Question?> CreateAsync(Question question)
        {
            question.CreatedAt = DateTime.Now;

            foreach(var answer in question.Answers) answer.CreatedAt = DateTime.Now;

            #region Done in Fluent Validation
            // Done in Fluent Validation
            //var topic = await unitOfWork.Repository<Topic>().GetByIdAsync(question.TopicId) 
            //            ?? throw new ValidationException("Topic Not Found!");
            //question.Topic = topic;

            //var trueAnswerCount = question.Answers.Count(x => x.IsRight == true);
            //if(trueAnswerCount != 1) throw new ValidationException("True answers Should be only one!");

            //if (question.Answers.Count == 0 || question.Answers.Count > 5) throw new ValidationException("Answers Count Should be in range 1-5"); 
            #endregion

            await unitOfWork.Repository<Question>().AddAsync(question);

            var rowsAffected = await unitOfWork.CompleteAsync();

            return rowsAffected > 0 ? question : null;
        }

        public async Task DeleteAsync(int questionId)
        {
            var question = (await GetByIdAsync(questionId))!;
            
            foreach(var answer in question.Answers)
            {
                answer.IsDeleted = true;
                answer.DeletedAt = DateTime.Now;
                unitOfWork.Repository<Answer>().Update(answer);
            }
            question.IsDeleted = true;
            question.DeletedAt = DateTime.Now;
            unitOfWork.Repository<Question>().Update(question);

            var affectedRows = await unitOfWork.CompleteAsync();
            if (affectedRows < question.Answers.Count + 1) throw new InvalidOperationException("some resources did not deleted successfully! ");
        }

        public async Task<Question> UpdateAsync(UpdateQuestionDto updateQuestionDto)
        {
            var existingQuestion = await GetByIdAsync(updateQuestionDto.Id);
            
            existingQuestion!.UpdatedAt = DateTime.Now;
            
            UpdateObjectHelper.UpdateObject(existingQuestion, updateQuestionDto);
            
            unitOfWork.Repository<Question>().Update(existingQuestion);

            for (int i = 0; i < updateQuestionDto.Answers.Count(); i++)
            {
                var existingAnswer = existingQuestion.Answers.ElementAt(i);
                var updatedAnswer = updateQuestionDto.Answers.ElementAt(i);

                existingAnswer.UpdatedAt = DateTime.Now;
                UpdateObjectHelper.UpdateObject(existingAnswer, updatedAnswer);
                unitOfWork.Repository<Answer>().Update(existingAnswer);
            }

            await unitOfWork.CompleteAsync();

            return existingQuestion;
        }

        public async Task<IReadOnlyList<Question>> GetExamQuestions(int examId)
        {
            var questionIds = (await unitOfWork.Repository<ExamQuestion>()
                .GetAllWithSpecAsync(new BaseSpecification<ExamQuestion>(x => x.ExamId == examId))).Select(x => x.QuestionId).ToList();

            var examQuestionsSpec = new ExamQuestionSpecification(questionIds);

            return await unitOfWork.Repository<Question>().GetAllWithSpecAsync(examQuestionsSpec);
        }

    }
}
