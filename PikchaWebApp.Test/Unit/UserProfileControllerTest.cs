using Microsoft.AspNetCore.Hosting;
using Moq;
using PikchaWebApp.Controllers;
using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using PikchaWebApp.Models;
using Microsoft.AspNetCore.Identity;
using PikchaWebApp.Data;
using PikchaWebApp.Test.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace PikchaWebApp.Test.Unit
{
    public class UserProfileControllerTest : IClassFixture<SqliteInMemoryFixture>
    {
        protected readonly UserManager<PikchaUser> _userManager;
        private readonly SqliteInMemoryFixture _fixture;

        public UserProfileControllerTest(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.CreateDatabase();
            //_userManager = new UserManager<PikchaUser>();
        }

        [Fact]
        public async Task Get_ReturnsJsonAListofUsers()
        {
            // Arrange
            var mockRepoWebHost = new Mock<IWebHostEnvironment>();
            var mockRepoConfig = new Mock<IConfiguration>();
            var mockRepoUserMan =  MockHelpers.MockUserManager<PikchaUser>();
            
            // create two test users
            Task user1Result =  MockHelpers.CreateNewUser("testuser1", "testuser1@test.thananji.com", "Test@123", _fixture);
            Task user2Result =  MockHelpers.CreateNewUser("testuser2", "testuser2@test.thananji.com", "Test@123", _fixture);
            Task.WaitAll(user1Result, user2Result);
            
            var userManager = _fixture.ServiceProvider.GetRequiredService<UserManager<PikchaUser>>();
            var controller = new UserProfileController(mockRepoWebHost.Object, mockRepoConfig.Object, userManager, _fixture.Context);

            // Act
            var result = await controller.Get();
            
            // Assert
            /*var viewResult = Assert.IsType<JsonResult>(result);
            var model = Assert.IsAssignableFrom<List<PikchaUser>>(
                viewResult.Value);
            Assert.Equal(2, model.Count); */
            var viewResult = Assert.IsType<ReturnDataModel>(result);
            Assert.NotNull(viewResult.Data);
            var users = Assert.IsType<List<PikchaUser>>(viewResult.Data);

            Assert.Equal(2, users.Count); 
        }

    }
}

// https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-2.2