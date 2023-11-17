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
        private readonly IQuestionService _questionService;
        private readonly IExamQuestionService _examQuestionService;

        public ExamsController( IExamService examService, IMapper mapper, IQuestionService questionService, IExamQuestionService examQuestionService)
        {
            this._examService = examService;
            this._mapper = mapper;
            this._questionService = questionService;
            this._examQuestionService = examQuestionService;
        }

        [Authorize]
        [ProducesResponseType(typeof(IReadOnlyList<ExamResponseDetailsDto>), StatusCodes.Status200OK)]
        [HttpGet("currentUserAllExamDetails")]
        public async Task<ActionResult<IReadOnlyList<ExamResponseDetailsDto>>> GetAllExamDetailsForUserAsync()
        {
            var userId = User.FindFirstValue("uid")!;

            var exams = await _examService.GetAllExamsForUserAsync(userId);

            var mappedExam = _mapper.Map<IReadOnlyList<ExamResponseDetailsDto>>(exams);

            return Ok(mappedExam);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Exam>> CreateExam(int certificateId)
        {
            var userId = User.FindFirstValue("uid")!;

            var createdExam = await _examService.CreateExamAsync(userId, certificateId);

            var mappedExam = _mapper.Map<ExamResponseDto>(createdExam);

            //return Ok(mappedExam);
            return Ok((await GetCurrentExamQuestions()).Result);
        }

        [Authorize]
        [ProducesResponseType(typeof(ExamResponseDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("currentActiveExamDetails")]
        public async Task<ActionResult<IReadOnlyList<ExamResponseDetailsDto>>> GetCurrentExamDetails()
        {
            var userId = User.FindFirstValue("uid")!;

            var exam = await _examService.GetCurrentExamDetailsAsync(userId);

            var mappedExam = _mapper.Map<ExamResponseDetailsDto>(exam);

            return Ok(mappedExam);
        }

        [Authorize]
        [ProducesResponseType(typeof(IReadOnlyList<ExamQuestionView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("currentActiveExamQuestions")]
        public async Task<ActionResult<IReadOnlyList<ExamQuestionView>>> GetCurrentExamQuestions()
        {
            var userId = User.FindFirstValue("uid")!;

            var exam = await _examService.GetCurrentExamDetailsAsync(userId);

            var questions = await _examService.GetCurrentExamQuestionsAsync(exam.Id);

            return Ok(questions);
        }


        [Authorize]
        [ProducesResponseType(typeof(ExamQuestionResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status401Unauthorized)]
        [HttpPut("currentActiveExamQuestions")]
        public async Task<ActionResult<ExamQuestionResponseDto>> UpdateExamQuestion(UpdateAnswerIdInExamQuestionDto updateExamQuestionDto)
        {
            var userId = User.FindFirstValue("uid")!;

            var exam = await _examService.GetCurrentExamDetailsAsync(userId);

            if (exam is null)
                return NotFound(new ApiErrorResponse(404, "No Currently Active Exams!"));

            if(DateTime.Now > exam.ExamEndTime.AddMinutes(5))
                return BadRequest(new ApiErrorResponse(400, "You can't update answers after ExamEndTime!"));

            if (exam.Id != updateExamQuestionDto.ExamId)
                return Unauthorized(new ApiErrorResponse(401, "You are unauthorized to update these resources!"));

            var updatedExamQuestion = await _examQuestionService.UpdateAnswerInExamQuestionAsync(updateExamQuestionDto);

            var mappedResult = _mapper.Map<ExamQuestionResponseDto>(updatedExamQuestion);

            return Ok(mappedResult);
        }


        [Authorize]
        [ProducesResponseType(typeof(ExamResponseDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status401Unauthorized)]
        [HttpPut("completeCurrentActiveExam")]
        public async Task<ActionResult<ExamResponseDetailsDto>> UpdateCompleteCurrentActiveExam()
        {
            var userId = User.FindFirstValue("uid")!;

            var exam = await _examService.UpdateCompleteExamAsync(userId);

            var mappedResult = _mapper.Map<ExamResponseDetailsDto>(exam);

            return Ok(mappedResult);
        }



        [HttpGet("{examId}")]
        public async Task<ActionResult<IReadOnlyList< ExamReview>>> GetExamReviews(int examId)
        {
            var userId = User.FindFirstValue("uid")!;

            var examReviews = await _examService.GetExamReviews(userId, examId);

            return Ok(examReviews);
        }
    }
}
