using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Dtos.ExamDtos
{
    public class UpdateCompleteExamDto
    {
        [Required]
        public int ExamId { get; set; }
    }
}
