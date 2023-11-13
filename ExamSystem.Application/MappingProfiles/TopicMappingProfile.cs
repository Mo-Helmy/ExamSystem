using AutoMapper;
using ExamSystem.Application.Dtos.TopicDtos;
using ExamSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.MappingProfiles
{
    public class TopicMappingProfile : Profile
    {
        public TopicMappingProfile()
        {
            CreateMap<Topic, TopicResponseDto>()
                .ForMember(x => x.TopicName, o => o.MapFrom(x => x.TopicName));
        }
    }
}
