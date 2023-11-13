using ExamSystem.Application.Services.Contract;
using ExamSystem.Application.Specifications.CertificateSpec;
using ExamSystem.Application.Specifications.CertificateTopicSpec;
using ExamSystem.Domain.Entities;
using ExamSystem.Infrastructure.Specifications;
using ExamSystem.Infrastructure.UnitOfWorks.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Services
{
    public class CertificateTopicService : GenericService<CertificateTopic>, ICertificateTopicService
    {
        public CertificateTopicService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<IReadOnlyList<CertificateTopic>> GetAllAsync(int certificateId)
        {
            var certificateSpec = new CertificateTopicSpecification(certificateId);

            var certificateTopics = await _unitOfWork.Repository<CertificateTopic>().GetAllWithSpecAsync(certificateSpec);

            return certificateTopics;
        }

    }
}
