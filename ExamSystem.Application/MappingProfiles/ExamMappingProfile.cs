using AutoMapper;
using ExamSystem.Application.Dtos.ExamDtos;
using ExamSystem.Application.Dtos.ExamQuestion;
using ExamSystem.Application.Dtos.TopicDtos;
using ExamSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.MappingProfiles
{
    public class ExamMappingProfile : Profile
    {
        public ExamMappingProfile()
        {
            CreateMap<Exam, ExamResponseDto>()
                .ForMember(x => x.ExamId, options => options.MapFrom(x => x.Id))
                .ForMember(x => x.Certificate, options => options.MapFrom(x => x.Certificate.CertificateName))
                .ForMember(x => x.TestDurationInMinutes, options => options.MapFrom(x => x.Certificate.TestDurationInMinutes))
                .ForMember(x => x.PassScore, options => options.MapFrom(x => x.Certificate.PassScore));

        }
    }
}
