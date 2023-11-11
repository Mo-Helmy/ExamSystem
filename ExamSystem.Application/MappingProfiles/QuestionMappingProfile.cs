using AutoMapper;
using ExamSystem.Application.Dtos.QuestionDtos;
using ExamSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Application.MappingProfiles
{
    public class QuestionMappingProfile : Profile
    {
        public QuestionMappingProfile()
        {
            CreateMap<QuestionDto, Question>().ReverseMap();
            CreateMap<AnswerDto, Answer>().ReverseMap();
        }
    }
}
