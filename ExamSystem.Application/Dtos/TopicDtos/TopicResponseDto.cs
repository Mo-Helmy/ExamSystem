using ExamSystem.Application.Dtos.AnswersDtos;
using ExamSystem.Application.Dtos.CertificateTopicDtos;
using ExamSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Dtos.TopicDtos
{
    public class TopicResponseDto
    {
        public int Id { get; set; }
        public string TopicName { get; set; }
    }
}
