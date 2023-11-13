using ExamSystem.Domain.Entities;
using ExamSystem.Infrastructure.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Specifications.CertificateSpec
{
    public class CertificateCountSpecification : BaseSpecification<Certificate>
    {
        public CertificateCountSpecification(CertificateSpecificationParams specificationParams)
        {
            if (!string.IsNullOrEmpty(specificationParams.Search))
                CriteriaList.Add(x => x.CertificateName.Contains(specificationParams.Search));
        }
    }
}
