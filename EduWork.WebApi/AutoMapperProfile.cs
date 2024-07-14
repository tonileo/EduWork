using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common.DTO;
using EduWork.DataAccessLayer.Entites;

namespace EduWork.BusinessLayer
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<ProjectTime, ProjectTimeDtoTest>()
            .ForMember(dest => dest.TitleProject, opt => opt.MapFrom(src => src.Project.Title))
            .ForMember(dest => dest.DateWorkDay, opt => opt.MapFrom(src => src.WorkDay.WorkDate));

            CreateMap<Project, ProjectSmallDto>();

            CreateMap<User, UsernamesDto>();

            CreateMap<ProjectTime, ProjectTimeDto>()
           .ForMember(dest => dest.TitleProject, opt => opt.MapFrom(src => src.Project.Title));

            CreateMap<List<ProjectTime>, ProjectTimeResponseDto>()
            .ForMember(dest => dest.ProjectTimes, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.ProjectTimeSums, opt => opt.Ignore())
            .ForMember(dest => dest.SumAllProjectTimesHours, opt => opt.MapFrom(src => src.Sum(pt => pt.TimeSpentMinutes) / 60))
            .ForMember(dest => dest.SumAllProjectTimesMinutes, opt => opt.MapFrom(src => src.Sum(pt => pt.TimeSpentMinutes) % 60));

            CreateMap<ProjectTimeRequestDto, ProjectTime>();
        }
    }
}
