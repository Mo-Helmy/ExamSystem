using ExamSystem.Domain.Entities;
using ExamSystem.Infrastructure.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Specifications.CertificateSpec
{
    public class CertificateSpecification : BaseSpecification<Certificate>
    {
        public CertificateSpecification(CertificateSpecificationParams specificationParams)
        {
            Includes.Add(x => x.CertificateTopis);
            Includes.Add(x => x.Topics);

            if (!string.IsNullOrEmpty(specificationParams.Search))
                CriteriaList.Add(x => x.CertificateName.Contains(specificationParams.Search));

            ApplyPagination(specificationParams.PageSize * (specificationParams.PageIndex - 1), specificationParams.PageSize);
        }

        public CertificateSpecification(int certificateId): base(x => x.Id == certificateId)
        {
            Includes.Add(x => x.CertificateTopis);
            Includes.Add(x => x.Topics);
        }
    }
}
