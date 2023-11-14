using ExamSystem.Application.Services.Contract;
using ExamSystem.Domain.Entities;
using ExamSystem.Infrastructure.UnitOfWorks.Contract;
using ExamSystem.Application.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ExamSystem.Infrastructure.Specifications;
using ExamSystem.Application.Helper;

namespace ExamSystem.Application.Services
{
    public class ExamService : GenericService<Exam>, IExamService
    {
        private readonly ICertificateService _certificateService;
        private readonly IQuestionService _questionService;

        public ExamService(IUnitOfWork unitOfWork, ICertificateService certificateService, IQuestionService questionService) : base(unitOfWork)
        {
            this._certificateService = certificateService;
            this._questionService = questionService;
        }

        public async Task<IReadOnlyList<Question>> CreateExamAsync(string userId, int certificateId)
        {
            var certificate = (await _certificateService.GetByIdAsync(certificateId))
                ?? throw new ValidationException("Certificate Id is not exist!");

            var exam = await base.CreateAsync(new Exam()
            {
                UserId = userId,
                CertificateId = certificateId,
                ExamStartTime = DateTime.Now,
                ExamEndTime = DateTime.Now.AddMinutes(certificate.TestDurationInMinutes),
                CreatedAt = DateTime.Now,
            });

            if (exam == null) throw new InvalidOperationException("create exam failed");

            //get topic questionIds
            List<int> questionsIds = new();

            // get topic ids and questionCount
            foreach (var certificateTopic in certificate.CertificateTopis)
            {
                List<int> topicQuestionsIds = new();

                var questionCount = certificateTopic.QuestionCount;

                var topicQuestions = (await _unitOfWork.Repository<Question>()
                        .GetAllWithSpecAsync(new BaseSpecification<Question>(x => x.TopicId == certificateTopic.TopicId)));

                AppHelpers.Shuffle<Question>((List<Question>)topicQuestions);


                foreach (var question in topicQuestions)
                {
                    topicQuestionsIds.Add(question.Id);
                    questionsIds.Add(question.Id);
                }

                //AppHelpers.Shuffle(topicQuestionsIds);

                // create random questions from this topic in ExamQuestions
                for (var i = 0; i < questionCount; i++)
                {
                    await _unitOfWork.Repository<ExamQuestion>().AddAsync(new ExamQuestion()
                    {
                        ExamId = exam.Id,
                        QuestionId = topicQuestionsIds.ElementAt(i),
                        CreatedAt = DateTime.Now,
                    });
                }
            }

            await _unitOfWork.CompleteAsync();

            var questions = await _questionService.GetExamQuestions(exam.Id);

            return questions;
        }
    }
}
