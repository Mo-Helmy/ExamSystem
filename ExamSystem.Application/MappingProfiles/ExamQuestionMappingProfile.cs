using AutoMapper;
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
    public class ExamQuestionMappingProfile : Profile
    {
        public ExamQuestionMappingProfile()
        {
            CreateMap<ExamQuestion, ExamQuestionResponseDto>();
        }
    }
}
