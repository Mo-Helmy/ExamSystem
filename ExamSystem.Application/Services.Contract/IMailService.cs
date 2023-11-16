using ExamSystem.Application.Dtos.MailDtos;
using ExamSystem.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Services.Contract
{
    public interface IMailService
    {
        BaseResponse<string> SendMail(MailRequest mailRequest);
    }
}
