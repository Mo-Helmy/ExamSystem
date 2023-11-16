using AutoMapper;
using ExamSystem.Application.Dtos.ExamDtos;
using ExamSystem.Application.Dtos.ExamQuestion;
using ExamSystem.Application.Dtos.QuestionDtos;
using ExamSystem.Application.Errors;
using ExamSystem.Application.Responses;
using ExamSystem.Application.Services;
using ExamSystem.Application.Services.Contract;
using ExamSystem.Application.Specifications.Questions;
using ExamSystem.Domain.Entities;
using ExamSystem.Domain.Entities.Identity;
using ExamSystem.Infrastructure.Specifications;
using ExamSystem.Infrastructure.UnitOfWorks.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExamSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly IExamService _examService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExamQuestionService _examQuestionService;

        public ExamsController(
            IExamService examService,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IExamQuestionService examQuestionService)
        {
            this._examService = examService;
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
            this._examQuestionService = examQuestionService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<IReadOnlyList<QuestionResponseDto>>> CreateExam(int certificateId)
        {
            var userId = User.FindFirstValue("uid")!;

            var currentActiveExam = await _unitOfWork.Repository<Exam>()
                .GetByIdWithSpecAsync(new BaseSpecification<Exam>(x => x.UserId == userId && x.ExamEndTime > DateTime.Now));

            if (currentActiveExam is not null)
                return Ok(await _unitOfWork.ExamRepository.GetExamQuestion(currentActiveExam.Id));

            var questions = await _unitOfWork.ExamRepository.CreateExamAsync(userId, certificateId);

            return Ok(questions);
        }

        [Authorize]
        [ProducesResponseType(typeof(IReadOnlyList<QuestionResponseDto>), StatusCodes.Status200OK)]
        [HttpGet("currentUserAllExamDetails")]
        public async Task<ActionResult<IReadOnlyList<ExamQuestionStoredProcedure>>> GetAllExamDetailsForUserAsync()
        {
            var userId = User.FindFirstValue("uid")!;

            var exams = await _examService.GetAllExamDetailsForUserAsync(userId);

            var mappedExam = _mapper.Map<IReadOnlyList<ExamResponseDto>>(exams);

            return Ok(mappedExam);
        }



        [Authorize]
        [ProducesResponseType(typeof(QuestionResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("currentActiveExamDetails")]
        public async Task<ActionResult<IReadOnlyList<ExamQuestionStoredProcedure>>> GetCurrentExamDetails()
        {
            var userId = User.FindFirstValue("uid")!;

            var exam = await _examService.GetCurrentExamDetailsAsync(userId);

            if (exam is null) return NotFound(new ApiErrorResponse(404, "No Currently Active Exams for you!"));

            var mappedExam = _mapper.Map<ExamResponseDto>(exam);

            return Ok(mappedExam);
        }


        [Authorize]
        [ProducesResponseType(typeof(IReadOnlyList<QuestionResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("currentActiveExamQuestions")]
        public async Task<ActionResult<IReadOnlyList<ExamQuestionStoredProcedure>>> GetCurrentExamQuestions()
        {
            var userId = User.FindFirstValue("uid")!;

            var exam = await _unitOfWork.Repository<Exam>().GetByIdWithSpecAsync(new BaseSpecification<Exam>(x => x.UserId == userId && x.ExamEndTime > DateTime.Now));

            if (exam is null) return NotFound(new ApiErrorResponse(404, "No Currently Active Exams for you!"));

            var questions = await _unitOfWork.ExamRepository.GetExamQuestion(exam.Id);

            return Ok(questions);
        }

        [Authorize]
        [ProducesResponseType(typeof(ExamQuestionResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status401Unauthorized)]
        [HttpPut("currentActiveExamQuestions")]
        //public async Task<ActionResult<IReadOnlyList<QuestionResponseDto>>> UpdateExamQuestion(int QuestionId, int AnswerId)
        public async Task<ActionResult<ExamQuestionResponseDto>> UpdateExamQuestion(UpdateAnswerIdInExamQuestionDto updateExamQuestionDto)
        {

            var userId = User.FindFirstValue("uid")!;

            var exam = await _unitOfWork.Repository<Exam>()
                .GetByIdWithSpecAsync(new BaseSpecification<Exam>(x => x.UserId == userId && x.ExamEndTime > DateTime.Now));

            if (exam is null)
                return NotFound(new ApiErrorResponse(404, "No Currently Active Exams!"));

            //var updateExamQuestionDto = new UpdateAnswerIdInExamQuestionDto() { ExamId = exam.Id, QuestionId = QuestionId, AnswerId = AnswerId };

            if (exam.Id != updateExamQuestionDto.ExamId)
                return Unauthorized(new ApiErrorResponse(401, "You are update protected resources"));

            var updatedExamQuestion = await _examQuestionService.UpdateAnswerInExamQuestionAsync(updateExamQuestionDto);

            var mappedResult = _mapper.Map<ExamQuestionResponseDto>(updatedExamQuestion);

            return Ok(mappedResult);
        }

        [Authorize]
        [ProducesResponseType(typeof(ExamResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status401Unauthorized)]
        [HttpPut("completeCurrentActiveExam")]
        public async Task<ActionResult<ExamResponseDto>> UpdateCompleteCurrentActiveExam()
        {
            var userId = User.FindFirstValue("uid")!;

            var exam = await _examService.UpdateCompleteExamAsync(userId);
            
            var mappedResult = _mapper.Map<ExamResponseDto>(exam);

            return Ok(mappedResult);
        }


        [Authorize]
        [ProducesResponseType(typeof(IReadOnlyList<ExamOverView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status401Unauthorized)]
        [HttpGet("examOverview/{examId}")]
        public async Task<ActionResult<IReadOnlyList<ExamOverView>>> GetExamOverview(int examId)
        {
            var userId = User.FindFirstValue("uid")!;

            var examOverview = await _examService.GetExamOverviewAsync(userId, examId);

            return Ok(examOverview);
        }
    }
}
