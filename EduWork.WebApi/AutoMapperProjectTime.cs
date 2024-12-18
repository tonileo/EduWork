﻿using AutoMapper;
using Common.DTO.Profile;
using Common.DTO.ProjectTime;
using EduWork.DataAccessLayer.Entites;

namespace EduWork.WebApi
{
    public class AutoMapperProjectTime : Profile
    {
        public AutoMapperProjectTime()
        {
            CreateMap<Project, ProjectSmallDto>()
                .ForMember(dest => dest.LastChosenTitle, opt => opt.MapFrom(src => ""));

            CreateMap<User, UsernamesDto>();

            CreateMap<ProjectTime, InputProjectTimeDto>()
                .ForMember(dest => dest.TitleProject, opt => opt.MapFrom(src => src.Project!.Title))
                .ForMember(dest => dest.DateWorkDay, opt => opt.MapFrom(src => src.WorkDay!.WorkDate))
                .ForMember(dest => dest.Hours, opt => opt.MapFrom(src => src.TimeSpentMinutes / 60))
                .ForMember(dest => dest.Minutes, opt => opt.MapFrom(src => src.TimeSpentMinutes % 60));

            CreateMap<ProjectTime, ProjectTimeDto>()
                .ForMember(dest => dest.TitleProject, opt => opt.MapFrom(src => src.Project!.Title));

            CreateMap<List<ProjectTime>, ProjectTimeResponseDto>()
                .ForMember(dest => dest.ProjectTimes, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.ProjectTimeSums, opt => opt.Ignore())
                .ForMember(dest => dest.SumAllProjectTimesHours, opt => opt.MapFrom(src => src.Sum(pt => pt.TimeSpentMinutes) / 60))
                .ForMember(dest => dest.SumAllProjectTimesMinutes, opt => opt.MapFrom(src => src.Sum(pt => pt.TimeSpentMinutes) % 60));

            CreateMap<ProjectTimeRequestDto, ProjectTime>();
        }
    }

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Project, ProjectsProfileDto>();

            CreateMap<User, UserProfileDto>()
                .ForMember(dest => dest.Project, opt => opt.Ignore());

            CreateMap<SickLeaveRecord, SickLeaveRecordDto>();

            CreateMap<AnnualLeave, AnnualLeaveDto>()
                .ForMember(dest => dest.LeftLeaveDaysLastYear, opt => opt.Ignore())
                .ForMember(dest => dest.PlannedLeaveDays, opt => opt.Ignore());

            CreateMap<AnnualLeaveRecord, AnnualLeaveRecordDto>();

            CreateMap<ProfileAnnualRequestDto, AnnualLeaveRecord>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.StartDate)))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.EndDate)));

            CreateMap<ProfileAnnualRequestDto, SickLeaveRecord>()
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.StartDate)))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.EndDate)));
        }
    }
}
