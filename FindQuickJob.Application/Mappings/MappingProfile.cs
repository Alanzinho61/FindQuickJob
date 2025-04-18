using AutoMapper;
using FindQuickJob.Application.Dtos;
using FindQuickJob.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindQuickJob.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Application <-> ApplicationDto
            CreateMap<Domain.Entities.Application, ApplicationDto>()
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.JobPostTitle, opt => opt.MapFrom(src => src.JopPost.Title));
            CreateMap<ApplicationDto, Domain.Entities.Application>();

            //CreateJobDto -> JobPost
            CreateMap<CreateJobPostDto,JobPost>();

            // JobPost <-> JobPostDto
            CreateMap<JobPost, JobPostDto>()
                .ForMember(dest => dest.CreatedByUserFullName, opt => opt.MapFrom(src => src.CreatedByUser.FullName));
            CreateMap<JobPostDto, JobPost>();
            
            // User <-> UserDto
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

        }
    }
}
