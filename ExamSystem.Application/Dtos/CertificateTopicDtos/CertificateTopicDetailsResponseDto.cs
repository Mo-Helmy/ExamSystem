using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Dtos.CertificateTopicDtos
{
    public class CertificateTopicDetailsResponseDto
    {
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public int QuestionCount { get; set; }
        public double TopicPercentage { get; set; }
    }
}
