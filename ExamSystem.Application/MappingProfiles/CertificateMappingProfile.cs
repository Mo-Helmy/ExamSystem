using AutoMapper;
using ExamSystem.Application.Dtos.CertificateDtos;
using ExamSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.MappingProfiles
{
    public class CertificateMappingProfile : Profile
    {
        public CertificateMappingProfile()
        {
            CreateMap<Certificate, CertificateResponseDto>();
            //CreateMap<Certificate, CertificateDetailsResponseDto>();
            //.ForMember(
            //dto => dto.TopicDetails.Select(x => x.TopicName), 
            //config => config.MapFrom(certificate => certificate.Topics.FirstOrDefault(x => x.Id == certificate.CertificateTopis.)) 
            //);

            CreateMap<AddCertificateDto, Certificate>();
        }
    }
}
