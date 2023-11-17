using AutoMapper;
using ExamSystem.Application.Dtos.CertificateTopicDtos;
using ExamSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.MappingProfiles
{
    public class CertificateTopicMappingProfile : Profile
    {
        public CertificateTopicMappingProfile()
        {
            CreateMap<CertificateTopic, CertificateTopicResponseDto>()
                .ForMember(x => x.TopicId, config => config.MapFrom(x => x.Topic.Id))
                .ForMember(x => x.TopicName, config => config.MapFrom(x => x.Topic.TopicName))
                .ForMember(x => x.TopicPercentage, config => config.MapFrom(x => x.TopicPercentage));

            CreateMap<AddCertificateTopicDto, CertificateTopic>();
        }
    }
}
