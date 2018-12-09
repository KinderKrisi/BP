using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using Services.IdentityServer;
using Data;

namespace Repositories.Test
{
    public class IntegrationTest
    {
        private readonly Mock<IUserInfoService> _userInfoServiceMock;

        public IntegrationTest()
        {
            _userInfoServiceMock = new Mock<IUserInfoService>();
        }
        [Fact]
        public async void IntegrationTestDeleteProfile()
        {
            var options = new DbContextOptionsBuilder<BefordingTestContext>()
            .UseInMemoryDatabase(databaseName: "BefordingTestDb")
            .Options;
            var context = new BefordingTestContext(options);

            var logRepository = new LogRepository(context, _userInfoServiceMock.Object);

            var profileRepository = new ProfileRepository(context, _userInfoServiceMock.Object, logRepository);

            var dummyHospital = new HospitalProfile()
            {
                Address = "Address",
                NameOfHospital = "UUU",
                Rate = 0
            };

            context.Profiles.Add(dummyHospital);
            await context.SaveChangesAsync();

            Assert.True(context.Profiles.FirstOrDefaultAsync(x => x.Address == "Address") != null);
            Assert.True(context.Profiles.FirstOrDefaultAsync(x => x.Id == 1) != null);
            Assert.True(await context.Profiles.CountAsync() == 1);


            var resultTest1 = context.Profiles.FirstOrDefaultAsync(x => x.Address == "Address");

            await profileRepository.DeleteProfileAdmin(1);
            Assert.True(await context.Profiles.CountAsync() == 0);

            Assert.True(await context.Logs.CountAsync() == 0);

            await profileRepository.DeleteProfileAdmin(-1);

            var resultTest2 = context.Logs.FirstOrDefaultAsync();

            Assert.True(await context.Logs.CountAsync() == 1);
        }

    }
}
