using ExamSystem.Application.Dtos.CertificateDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Dtos.CertificateTopicDtos
{
    public class CertificateTopicResponseDto
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public int QuestionCount { get; set; }
        public decimal TopicPercentage { get; set; }
    }
}
