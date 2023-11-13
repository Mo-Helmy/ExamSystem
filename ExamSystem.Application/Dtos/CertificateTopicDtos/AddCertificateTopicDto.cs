using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Dtos.CertificateTopicDtos
{
    public class AddCertificateTopicDto
    {
        public int CertificateId { get; set; }
        
        public int TopicId { get; set; }
        
        public int QuestionCount { get; set; }
        
        public decimal TopicPercentage { get; set; }
    }
}
