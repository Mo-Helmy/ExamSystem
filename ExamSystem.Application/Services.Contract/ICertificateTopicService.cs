using ExamSystem.Application.Specifications.CertificateSpec;
using ExamSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Services.Contract
{
    public interface ICertificateTopicService : IGenericService<CertificateTopic>
    {
        Task<IReadOnlyList<CertificateTopic>> GetAllAsync(int certificateId);

    }
}
