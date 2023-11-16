using ExamSystem.Application.Dtos.MailDtos;
using ExamSystem.Application.Responses;
using ExamSystem.Application.Services.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailsController : ControllerBase
    {
        private readonly IMailService _mailService;

        public MailsController(IMailService mailService)
        {
            this._mailService = mailService;
        }


        [HttpPost]
        public ActionResult<BaseResponse<string>> SendMail([FromForm]MailRequest mailRequest)
        {
            return _mailService.SendMail(mailRequest);
        }
    }
}
