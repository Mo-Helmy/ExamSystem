using ExamSystem.Application.Services.Contract;
using ExamSystem.Application.Specifications.CertificateSpec;
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
    public class CertificateService : GenericService<Certificate>, ICertificateService
    {
        public CertificateService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<IReadOnlyList<Certificate>> GetAllAsync(CertificateSpecificationParams specificationParams)
        {
            var certificateSpec = new CertificateSpecification(specificationParams);

            var specifications = await _unitOfWork.Repository<Certificate>().GetAllWithSpecAsync(certificateSpec);

            return specifications;
        }

        public async Task<int> GetAllCountAsync(CertificateSpecificationParams specificationParams)
        {
            var certificateSpec = new CertificateCountSpecification(specificationParams);

            return await _unitOfWork.Repository<Certificate>().GetCountWithSpecAsync(certificateSpec);
        }

        public override async Task<Certificate?> GetByIdAsync(int id)
        {
            var certificateSpec = new CertificateSpecification(id);

            return await _unitOfWork.Repository<Certificate>().GetByIdWithSpecAsync(certificateSpec);
        }

    }
}
