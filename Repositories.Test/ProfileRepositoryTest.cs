using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;
using Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using Moq;
using Repositories.Interfaces;
using Services.IdentityServer;
using Xunit;

namespace Repositories.Test
{
    public class ProfileRepositoryTest
    {
        private readonly Mock<IUserInfoService> _userInfoServiceMock;
        private readonly Mock<ILogRepository> _logRepositoryMock;

        public ProfileRepositoryTest()
        {
            _userInfoServiceMock = new Mock<IUserInfoService>();
            _logRepositoryMock = new Mock<ILogRepository>();
        }

        [Fact]
        public async void CreateProfileTest()
        {
            var options = new DbContextOptionsBuilder<BefordingTestContext>()
                .UseInMemoryDatabase(databaseName: "BefordingTestDb")
                .Options;
            var context = new BefordingTestContext(options);

            var repository = new ProfileRepository(context, _userInfoServiceMock.Object, _logRepositoryMock.Object);

            var dummyHospitalVM = new HospitalProfileVM()
            {
                Address = "a",
                NameOfHospital = "UUU",
                Rate = 1
            };
            var result = await repository.CreateProfile(dummyHospitalVM);

            Assert.True(result.GetType() == typeof(HospitalProfile));
            Assert.True(result.Id == 1);
        }

        [Fact]
        public async void DeleteProfileTest()
        {
            var options = new DbContextOptionsBuilder<BefordingTestContext>()
                .UseInMemoryDatabase(databaseName: "BefordingTestDb")
                .Options;
            var context = new BefordingTestContext(options);

            var repository = new ProfileRepository(context, _userInfoServiceMock.Object, _logRepositoryMock.Object);

            fillMockDatabase(5, context);

            await repository.DeleteProfile(2);

            Assert.True(context.Profiles.FirstOrDefault(x => x.Id == 2) == null);
        }


        private async void fillMockDatabase(int numberOfElements, BefordingTestContext context)
        {
            for (int i = 0; i < numberOfElements; i++)
            {
                var dummyHospital= new HospitalProfile()
                {
                    Address = "" + i, 
                    NameOfHospital = "UUU",
                    Rate = i
                };
                context.Profiles.Add(dummyHospital);
                await context.SaveChangesAsync();
            }
        }
    }
}
