using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EduWork.BusinessLayer.Services;
using EduWork.DataAccessLayer;
using Moq;

namespace EduWork.UnitTests
{
    public class UserProjectTimeServiceTests
    {
        [Fact]
        public void Ctor_UserProjectTimeService_Succeeds()
        {
            var context = new Mock<AppDbContext>().Object;
            var mapper = new Mock<IMapper>().Object;

            var sut = new UserProjectTimeService(context, mapper);

            Assert.NotNull(sut);
        }

        [Fact]
        public async Task Ctor_UserProjectTimeService_Fails()
        {
            var context = new Mock<AppDbContext>().Object;
            var mapper = new Mock<IMapper>().Object;

            var sut = new UserProjectTimeService(context, mapper);

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await sut.GetProjectTimesFilter(null, null, null, null));
        }
    }
}
