using AutoMapper;
using ExamSystem.Application.Dtos.QuestionDtos;
using ExamSystem.Application.Responses;
using ExamSystem.Application.Services;
using ExamSystem.Application.Services.Contract;
using ExamSystem.Application.Specifications.Questions;
using ExamSystem.Domain.Entities.Identity;
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
        private readonly UserManager<AppUser> _userManager;
        private readonly IExamService _examService;
        private readonly IMapper _mapper;
        private readonly IQuestionService _questionService;

        public ExamsController(UserManager<AppUser> userManager, IExamService examService, IMapper mapper, IQuestionService questionService)
        {
            this._userManager = userManager;
            this._examService = examService;
            this._mapper = mapper;
            this._questionService = questionService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<IReadOnlyList<QuestionResponseDto>>> CreateExam(int certificateId)
        {
            var userId = User.FindFirstValue("uid")!;

            var questions = await _examService.CreateExamAsync(userId, certificateId);

            var mappedQuestions = _mapper.Map<IReadOnlyList<QuestionResponseDto>>(questions);

            return Ok(mappedQuestions);
        }



        [HttpGet("{examId}")]
        public async Task<ActionResult<IReadOnlyList< QuestionResponseDto>>> GetQuestionsByExamId(int examId)
        {
            var questions = await _questionService.GetExamQuestions(examId);

            var mappedResult = _mapper.Map<IReadOnlyList<QuestionResponseDto>>(questions);

            return Ok(mappedResult);
        }
    }
}
