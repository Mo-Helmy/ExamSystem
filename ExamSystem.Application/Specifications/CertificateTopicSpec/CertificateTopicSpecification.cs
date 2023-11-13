using ExamSystem.Domain.Entities;
using ExamSystem.Infrastructure.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.Specifications.CertificateTopicSpec
{
    public class CertificateTopicSpecification : BaseSpecification<CertificateTopic>
    {
        //public CertificateTopicSpecification()
        //{
        //    Includes.Add(x => x.Topic);
        //}

        public CertificateTopicSpecification(int certificateId) : base(x => x.CertificateId == certificateId)
        {
            Includes.Add(x => x.Topic);
        }
    }
}
