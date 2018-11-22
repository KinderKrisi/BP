using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using Services.IdentityServer;
using Data;
using Repositories.Interfaces;

namespace Repositories.Test
{
    public class LogRepositoryTest
    {

        private readonly Mock<IUserInfoService> _userServiceMock;

        public LogRepositoryTest()
        {
            _userServiceMock = new Mock<IUserInfoService>();
        }

        [Fact]
        public async void AddLogTest()
        {
            var options = new DbContextOptionsBuilder<BefordingTestContext>()
                .UseInMemoryDatabase(databaseName: "BefordingTestDb")
                .Options;
            var context = new BefordingTestContext(options);

            var repository = new LogRepository(context, _userServiceMock.Object);

            await repository.AddLog("123", "Test message");
            Assert.True(context.Logs.FirstOrDefault(x => x.UserId == "123") != null);
        }
    }
}
