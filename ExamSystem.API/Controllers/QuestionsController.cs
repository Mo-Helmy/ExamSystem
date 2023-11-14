using AutoMapper;
using ExamSystem.Application.Dtos.QuestionDtos;
using ExamSystem.Application.Errors;
using ExamSystem.Application.Responses;
using ExamSystem.Application.Services;
using ExamSystem.Application.Specifications.Questions;
using ExamSystem.Application.Specifications.TopicSpec;
using ExamSystem.Domain.Entities;
using FluentValidation;
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
        private readonly IValidator<AddQuestionDto> validator;

        public QuestionsController(IMapper mapper, IQuestionService questionService, IValidator<AddQuestionDto> validator)
        {
            this.mapper = mapper;
            this.questionService = questionService;
            this.validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<QuestionResponseDto>>> GetAll([FromQuery] QuestionSpecificationParams specificationParams)
        {
            var questions = await questionService.GetAllPaginatedAsync(specificationParams);

            var mappedResult = mapper.Map<IReadOnlyList<QuestionResponseDto>>(questions);

            var totalCount = await questionService.GetAllCountPaginatedAsync(specificationParams);

            return Ok(new Pagination<QuestionResponseDto>(specificationParams.PageIndex, specificationParams.PageSize, totalCount, mappedResult));
        }


        [ProducesResponseType(typeof(QuestionResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{QuestionId}")]
        public async Task<ActionResult<QuestionResponseDto>> GetById(int questionId)
        {
            var question = await questionService.GetByIdAsync(questionId);

            var mappedResult = mapper.Map<QuestionResponseDto>(question);

            return mappedResult is not null ? Ok(mappedResult) : NotFound(new ApiErrorResponse(404));
        }


        [ProducesResponseType(typeof(QuestionResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<QuestionResponseDto>> Create(AddQuestionDto addQuestionDto)
        {
            var question = mapper.Map<Question>(addQuestionDto);

            var createdQuestion = await questionService.CreateAsync(question);

            var mappedResult = mapper.Map<QuestionResponseDto>(createdQuestion);

            return mappedResult is not null ? StatusCode(201,mappedResult) : BadRequest(new ApiErrorResponse(400));
        }


        [ProducesResponseType(typeof(QuestionResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpPut]
        public async Task<ActionResult<QuestionResponseDto>> Update(UpdateQuestionDto updateQuestionDto)
        {
            //var question = mapper.Map<Question>(updateQuestionDto);

            var question = await questionService.UpdateAsync(updateQuestionDto);

            var mappedResult = mapper.Map<QuestionResponseDto>(question);

            return mappedResult is not null ? StatusCode(200, mappedResult) : BadRequest(new ApiErrorResponse(400));
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<QuestionResponseDto>> Delete([FromRoute] DeleteQuestionDto deleteQuestionDto)
        {
            await questionService.DeleteAsync(deleteQuestionDto.Id);

            return NoContent();
        }


    }
}

