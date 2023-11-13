using ExamSystem.Application.Dtos.AnswersDtos;
using ExamSystem.Application.Dtos.CertificateTopicDtos;
using ExamSystem.Application.Dtos.TopicDtos;
using ExamSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Dtos.CertificateDtos
{
    public class CertificateDetailsResponseDto
    {
        public int Id { get; set; }
        public string CertificateName { get; set; } = null!;
        public int TestDurationInMinutes { get; set; }
        public decimal PassScore { get; set; }

        public List<CertificateTopicDetailsResponseDto> TopicDetails { get; set; }

    }
}
