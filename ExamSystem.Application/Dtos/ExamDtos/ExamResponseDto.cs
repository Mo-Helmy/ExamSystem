using ExamSystem.Domain.Entities.Identity;
using ExamSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Dtos.ExamDtos
{
    public class ExamResponseDto
    {
        public int Id { get; set; }
        public int CertificateId { get; set; }

        public DateTime ExamStartTime { get; set; }
        public DateTime ExamEndTime { get; set; }
        public DateTime? ExamCompletedTime { get; set; }

        public bool? IsPassed { get; set; }
        public decimal? ExamScore { get; set; }
    }
}
