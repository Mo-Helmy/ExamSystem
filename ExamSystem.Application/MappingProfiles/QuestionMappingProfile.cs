using AutoMapper;
using ExamSystem.Application.Dtos.AnswersDtos;
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
            CreateMap<QuestionResponseDto, Question>().ReverseMap();
            CreateMap<AnswerResponseDto, Answer>().ReverseMap();

            CreateMap<AddQuestionDto, Question>();
            CreateMap<AddAnswerDto, Answer>();

            CreateMap<UpdateQuestionDto, Question>();
            CreateMap<UpdateAnswerDto, Answer>();

            CreateMap<UpdateQuestionDto, QuestionResponseDto>().ReverseMap();
            CreateMap<UpdateAnswerDto, QuestionResponseDto>().ReverseMap();
        }
    }
}
