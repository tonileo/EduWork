using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduWork.BusinessLayer.Services;

namespace EduWork.UnitTests
{
    public class UserProjectTimeServiceTests
    {
        [Fact]
        public void Ctor_UserProjectTimeService_Succeeds()
        {
            var sut = new UserProjectTimeService(null, null);

            Assert.NotNull(sut);
        }

        [Fact]
        public async Task Ctor_UserProjectTimeService_Fails()
        {
            var sut = new UserProjectTimeService(null, null);

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await sut.GetProjectTimesFilter(null, null, null, null));
        }
    }
}
