using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EduWork.BusinessLayer.Services;
using EduWork.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace EduWork.UnitTests
{
    public class UserProfileServiceTests
    {
        //private DbContextOptions<AppDbContext> options;
        //private AppDbContext context;
        //private UserProfileService service;

        //public UserProfileServiceTests()
        //{
        //    options = new DbContextOptionsBuilder<AppDbContext>()
        //        .UseInMemoryDatabase(databaseName: "UserProfileServiceTests")
        //        .Options;

        //    context = new AppDbContext(options);
        //    service = new UserProfileService(context);

        //    SeedDatabase();
        //}

        [Fact]
        public void Ctor_UserProfileService_Succeeds()
        {
            var context = new Mock<AppDbContext>().Object;
            var mapper = new Mock<IMapper>().Object;

            var sut = new UserProfileService(context, mapper);

            Assert.NotNull(sut);
        }

        [Fact]
        public async Task Ctor_UserProfileService_Fails()
        {
            var context = new Mock<AppDbContext>().Object;
            var mapper = new Mock<IMapper>().Object;

            var sut = new UserProfileService(context, mapper);

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await sut.GetUserProfile(null));
        }

    }
}
