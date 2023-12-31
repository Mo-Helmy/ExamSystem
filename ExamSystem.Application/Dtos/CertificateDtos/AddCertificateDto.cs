﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Dtos.CertificateDtos
{
    public class AddCertificateDto
    {
        [Required]
        public string CertificateName { get; set; } = null!;

        [Required]
        [Range(5, 240)]
        public int TestDurationInMinutes { get; set; }

        [Required]
        [Range(1, 100)]
        public decimal PassScore { get; set; }
    }
}
