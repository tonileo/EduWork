using AutoMapper;
using EduWork.BusinessLayer.Services;
using EduWork.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace EduWork.UnitTests
{
    public class UserProjectTimeServiceTests
    {
        [Fact]
        public void Ctor_UserProjectTimeService_Succeeds()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "EduWorkTestDb")
                .Options;

            var context = new AppDbContext(options);
            var mapper = new Mock<IMapper>().Object;

            var sut = new UserProjectTimeService(context, mapper);

            Assert.NotNull(sut);
        }

        [Fact]
        public async Task Ctor_UserProjectTimeService_Fails()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "EduWorkTestDb")
                .Options;

            var context = new AppDbContext(options);
            var mapper = new Mock<IMapper>().Object;

            var sut = new UserProjectTimeService(context, mapper);

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await sut.GetProjectTimesFilter(null, null, null, null));
        }
    }
}
