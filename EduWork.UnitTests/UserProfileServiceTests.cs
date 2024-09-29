using AutoMapper;
using Common.DTO.Profile;
using EduWork.BusinessLayer.Services;
using EduWork.DataAccessLayer;
using EduWork.DataAccessLayer.Entites;
using EduWork.DataAccessLayer.Seed;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace EduWork.UnitTests
{
    public class UserProfileServiceTests
    {
        private readonly DbContextOptions<AppDbContext> options;
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        private readonly UserProfileService service;

        public UserProfileServiceTests()
        {
            options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "EduWorkTestDb")
                .Options;

            context = new AppDbContext(options);

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Project, ProjectsProfileDto>();

                cfg.CreateMap<User, UserProfileDto>()
                    .ForMember(dest => dest.Project, opt => opt.Ignore());

                cfg.CreateMap<SickLeaveRecord, SickLeaveRecordDto>();

                cfg.CreateMap<AnnualLeave, AnnualLeaveDto>()
                    .ForMember(dest => dest.LeftLeaveDaysLastYear, opt => opt.Ignore())
                    .ForMember(dest => dest.PlannedLeaveDays, opt => opt.Ignore());

                cfg.CreateMap<AnnualLeaveRecord, AnnualLeaveRecordDto>();

                cfg.CreateMap<ProfileAnnualRequestDto, AnnualLeaveRecord>()
                    .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.StartDate)))
                    .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.EndDate)));

                cfg.CreateMap<ProfileAnnualRequestDto, SickLeaveRecord>()
                    .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.StartDate)))
                    .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.EndDate)));
            });

            mapper = mapperConfig.CreateMapper();

            service = new UserProfileService(context, mapper);

            SeedDatabase();
        }

        private void SeedDatabase()
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var seed = new SeedData(context);
            seed.Run();
        }

        [Fact]
        public async Task GetUserSmallProfile_ShouldReturnUserSmallProfile()
        {
            var result = await service.GetUserSmallProfiles("alice.johnson", true);

            Assert.NotNull(result);
            Assert.Contains(result, P => P.Id == 3);
            Assert.Contains(result, P => P.Email == "alice.johnson@example.com");
        }

        [Fact]
        public async Task GetAllUserProfiles_ShouldReturnAllProfiles()
        {
            var result = await service.GetUserSmallProfiles(null, null);

            Assert.NotNull(result);
            Assert.Contains(result, P => P.Id == 1);
            Assert.Contains(result, P => P.Email == "alice.johnson@example.com");
            Assert.Contains(result, P => P.Id == 2);
            Assert.Contains(result, P => P.Username == "jane.smith");
        }

        [Fact]
        public async Task GetUserCurrentProject_ShouldReturnUserCurrentProject()
        {
            var result = await service.GetUserCurrentProject(3);

            Assert.NotNull(result);
            Assert.NotNull(result.Title);
            Assert.NotNull(result.Role);
        }

        [Fact]
        public async Task GetMyProfileStats_ShouldReturnMyProfileStats()
        {
            var result = await service.GetMyProfileStats("alice.johnson", false, true);

            Assert.NotNull(result);
            Assert.IsType<MyProfileStatsDto>(result);
        }

        [Fact]
        public async Task GetUserProfile_ShouldReturnUserProfile()
        {
            var result = await service.GetUserProfile("alice.johnson");

            Assert.NotNull(result);
            Assert.Equal(3, result.Id);
            Assert.Equal(22, result.AnnualLeave?.TotalLeaveDays);
        }

        [Fact]
        public void Ctor_UserProfileService_Succeeds()
        {
            var contextTest = new Mock<AppDbContext>(options).Object;
            var mapperTest = new Mock<IMapper>().Object;

            var sut = new UserProfileService(contextTest, mapperTest);

            Assert.NotNull(sut);
        }

        [Fact]
        public async Task Ctor_UserProfileService_Fails()
        {
            var contextTest = new Mock<AppDbContext>(options).Object;
            var mapperTest = new Mock<IMapper>().Object;

            var sut = new UserProfileService(contextTest, mapperTest);

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await sut.GetUserProfile(null));
        }
    }
}
