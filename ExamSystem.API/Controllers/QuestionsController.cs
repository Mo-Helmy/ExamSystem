using AutoMapper;
using ExamSystem.API.Responses;
using ExamSystem.Application.Dtos.QuestionDtos;
using ExamSystem.Application.Services;
using ExamSystem.Application.Specifications.Questions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IQuestionService questionService;

        public QuestionsController(IMapper mapper, IQuestionService questionService)
        {
            this.mapper = mapper;
            this.questionService = questionService;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<QuestionDto>>> GetAll ([FromQuery] QuestionSpecificationParams specificationParams)
        {

            var questions = await questionService.GetAllPaginatedAsync(specificationParams);

            var mappedResult = mapper.Map<IReadOnlyList<QuestionDto>>(questions);

            var totalCount = await questionService.GetAllCountPaginatedAsync(specificationParams);

            return Ok(new Pagination<QuestionDto>(specificationParams.PageIndex, specificationParams.PageSize, totalCount, mappedResult));
        }


    }
}

