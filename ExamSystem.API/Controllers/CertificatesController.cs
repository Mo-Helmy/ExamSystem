using AutoMapper;
using ExamSystem.Application.Dtos.CertificateDtos;
using ExamSystem.Application.Dtos.CertificateTopicDtos;
using ExamSystem.Application.Errors;
using ExamSystem.Application.Responses;
using ExamSystem.Application.Services.Contract;
using ExamSystem.Application.Specifications.CertificateSpec;
using ExamSystem.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificatesController : ControllerBase
    {
        private readonly ICertificateService _certificateService;
        private readonly IMapper _mapper;
        private readonly ICertificateTopicService _certificateTopicService;

        public CertificatesController(ICertificateService certificateService, IMapper mapper, ICertificateTopicService certificateTopicService)
        {
            this._certificateService = certificateService;
            this._mapper = mapper;
            this._certificateTopicService = certificateTopicService;
        }


        #region Certificate

        [HttpGet]
        public async Task<ActionResult<Pagination<CertificateResponseDto>>> GetAllPagination([FromQuery] CertificateSpecificationParams specificationParams)
        {
            var certificates = await _certificateService.GetAllAsync(specificationParams);

            var mappedCertificates = _mapper.Map<IReadOnlyList<CertificateResponseDto>>(certificates);

            var totalCount = await _certificateService.GetAllCountAsync(specificationParams);

            return Ok(new Pagination<CertificateResponseDto>(specificationParams.PageIndex, specificationParams.PageIndex, totalCount, mappedCertificates));
        }

        [ProducesResponseType(typeof(CertificateResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CertificateResponseDto>> GetById(int id)
        {
            var certificate = await _certificateService.GetByIdAsync(id);

            var mappedCertificate = _mapper.Map<CertificateResponseDto>(certificate);

            return certificate is not null ? Ok(mappedCertificate) : NotFound(new ApiErrorResponse(404));
        }

        [ProducesResponseType(typeof(CertificateResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<CertificateResponseDto>> Create(AddCertificateDto addCertificateDto)
        {
            var entity = _mapper.Map<Certificate>(addCertificateDto);

            var certificate = await _certificateService.CreateAsync(entity);

            var mappedCertificate = _mapper.Map<CertificateResponseDto>(certificate);

            return certificate is not null ? Ok(mappedCertificate) : BadRequest(new ApiErrorResponse(400));
        }

        [ProducesResponseType(typeof(CertificateResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpPut]
        public async Task<ActionResult<CertificateResponseDto>> Update(UpdateCertificateDto updateCertificateDto)
        {
            var certificate = await _certificateService.UpdateAsync(updateCertificateDto.Id, updateCertificateDto);

            var mappedCertificate = _mapper.Map<CertificateResponseDto>(certificate);

            return mappedCertificate is not null ? StatusCode(200, mappedCertificate) : BadRequest(new ApiErrorResponse(400));
        }


        [ProducesResponseType(typeof(CertificateResponseDto), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpDelete("{certificateId}")]
        public async Task<ActionResult> Delete(int certificateId)
        {
            var result = await _certificateService.DeleteAsync(certificateId);

            return result ? StatusCode(204) : BadRequest(new ApiErrorResponse(400));
        }
        #endregion

        [HttpGet("{certificateId}/Topics")]
        public async Task<ActionResult<CertificateTopicResponseDto>> GetAllCertificateTopics(int certificateId)
        {
            var certificateTopics = await _certificateTopicService.GetAllAsync(certificateId);

            var mappedCertificateTopics = _mapper.Map<IReadOnlyList<CertificateTopicResponseDto>>(certificateTopics);

            return Ok(mappedCertificateTopics);
        }

        [ProducesResponseType(typeof(CertificateTopicResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpPost("Topics")]
        public async Task<ActionResult<CertificateTopicResponseDto>> Create([FromBody]AddCertificateTopicDto addCertificateTopicDto)
        {
            var entity = _mapper.Map<CertificateTopic>(addCertificateTopicDto);

            var certificateTopic = await _certificateTopicService.CreateAsync(entity);

            var mappedCertificateTopic = _mapper.Map<CertificateTopicResponseDto>(certificateTopic);

            return certificateTopic is not null ? Ok(mappedCertificateTopic) : BadRequest(new ApiErrorResponse(400));
        }


        [ProducesResponseType(typeof(CertificateTopicResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpPut("Topics")]
        public async Task<ActionResult<CertificateTopicResponseDto>> Update([FromBody]UpdateCertificateTopicDto updateCertificateTopicDto)
        {
            var certificateTopic = await _certificateTopicService.UpdateAsync(updateCertificateTopicDto.Id, updateCertificateTopicDto);

            var mappedCertificateTopic = _mapper.Map<CertificateTopicResponseDto>(certificateTopic);

            return certificateTopic is not null ? Ok(mappedCertificateTopic) : BadRequest(new ApiErrorResponse(400));
        }


        [ProducesResponseType(typeof(CertificateTopicResponseDto), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpDelete("Topics/{certificateTopicId}")]
        public async Task<ActionResult> DeleteCertificateTopic(int certificateTopicId)
        {
            var result = await _certificateTopicService.DeleteAsync(certificateTopicId);

            return result ? StatusCode(204) : BadRequest(new ApiErrorResponse(400));
        }
    }
}
