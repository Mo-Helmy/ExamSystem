using AutoMapper;
using ExamSystem.Application.Dtos.Authentication;
using ExamSystem.Domain.Entities.Identity;

namespace JobResearchSystem.Application.Mapping.ApplicationUsers
{
    public class ApplicationUserMappingProfile : Profile
    {
        public ApplicationUserMappingProfile()
        {
            CreateMap<AppUser, ResponseUserDetailsDto>();

            CreateMap<AppUser, ResponseUserDto>();

            CreateMap<UpdateUserDetailsDto, AppUser>();
        }
    }
}
