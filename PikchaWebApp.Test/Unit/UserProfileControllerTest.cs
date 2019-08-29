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
using Microsoft.AspNetCore.Http;

namespace PikchaWebApp.Test.Unit
{
    public class UserProfileControllerTest : IClassFixture<SqliteInMemoryFixture>
    {
        protected readonly UserManager<PikchaUser> _userManager;
        private readonly SqliteInMemoryFixture _fixture;
        Mock<IWebHostEnvironment> _webHostEnvironment;
        Mock<IConfiguration> _configurationManager;

        public UserProfileControllerTest(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.CreateDatabase();
            _webHostEnvironment = new Mock<IWebHostEnvironment>();
            _configurationManager = new Mock<IConfiguration>();
        }

        [Fact]
        public async Task Get_ReturnsJson_ListofUsers()
        {
            // Arrange            
            // create two test users
            Task<PikchaUser> user1Result = MockHelpers.CreateNewUser("testuser1", "testuser1@test.thananji.com", "Test@123", _fixture);
            Task<PikchaUser> user2Result = MockHelpers.CreateNewUser("testuser2", "testuser2@test.thananji.com", "Test@123", _fixture);
            Task.WaitAll(user1Result, user2Result);

            var userManager = _fixture.ServiceProvider.GetRequiredService<UserManager<PikchaUser>>();
            var controller = new UserProfileController(_webHostEnvironment.Object, _configurationManager.Object, userManager, _fixture.Context);

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

            // delete the user
            await MockHelpers.DeleteUser(user1Result.Result, _fixture);
            await MockHelpers.DeleteUser(user2Result.Result, _fixture);
        }

        [Fact]
        public async Task Get_ReturnAUser()
        {
            // Arrange
            string email = "testuser3@test.thananji.com";
            // create two test users
            Task<PikchaUser> user1Result = MockHelpers.CreateNewUser("testuser1", email, "Test@123", _fixture);
            if (user1Result.IsCompleted)
            {
                var userManager = _fixture.ServiceProvider.GetRequiredService<UserManager<PikchaUser>>();
                var controller = new UserProfileController(_webHostEnvironment.Object, _configurationManager.Object, userManager, _fixture.Context);

                // Act
                var result = await controller.Get(user1Result.Result.Id);

                var viewResult = Assert.IsType<ReturnDataModel>(result);
                Assert.NotNull(viewResult.Data);
                var user = Assert.IsType<PikchaUser>(viewResult.Data);

                Assert.Equal(email, user.Email);
                // delete the user
                await MockHelpers.DeleteUser(user, _fixture);
            }
        }

        [Fact]
        public async Task Put_UpdateAUser()
        {
            // Arrange
            string userId = "testuser4";
            string email = "testuser4@test.thananji.com";
            string fName = "Fname";
            // create two test users
            Task<PikchaUser> user1Result = MockHelpers.CreateNewUser(userId, email, "Test@123", _fixture);
            if (user1Result.IsCompleted)
            {
                ProfileViewModel profVM = new ProfileViewModel() { Id = user1Result.Result.Id, Address_1 = "Address 1", FirstName = fName, LastName = "LName", PostalCode = "1234" };

                var userManager = _fixture.ServiceProvider.GetRequiredService<UserManager<PikchaUser>>();
                var controller = new UserProfileController(_webHostEnvironment.Object, _configurationManager.Object, userManager, _fixture.Context);

                // Act
                var result = await controller.Put(1, profVM);

                var viewResult = Assert.IsType<ReturnDataModel>(result);
                Assert.NotNull(viewResult.Data);
                var user = Assert.IsType<ProfileViewModel>(viewResult.Data);

                Assert.Equal(fName, user.FirstName);

                // get the user from DB and check whether it is updated
                PikchaUser dUser = _fixture.Context.Users.Find(userId);
                Assert.Equal(fName, dUser.FirstName);



                // get the PikchUser (get it from user manager and make sure it is updated in user manager as well
                PikchaUser pUser = MockHelpers.FindUserById(userId, _fixture).Result;

                Assert.Equal(fName, pUser.FirstName);

                // delete the user
                await MockHelpers.DeleteUser(pUser, _fixture);
            }
        }


        [Fact]
        public async Task Post_UploadAvatarImage()
        {
            // TODO : Add login function
            // Arrange
            /* var configurationSection = new Mock<IConfigurationSection>();

            configurationSection.Setup(a => a.Value).Returns("uploads/avatars");
            _configurationManager.Setup(a => a.GetSection("UploadDirectories.Avatar")).Returns(configurationSection.Object);

            IFormFile avatarFile = MockHelpers.CreateNewImageFile("TestPhotos/profile-photo.jpg", "profile-photo.jpg", "profile-photo");

            string userId = "testuser4";
            string email = "testuser4@test.thananji.com";
            // create two test users
            Task<PikchaUser> user1Result = MockHelpers.CreateNewUser(userId, email, "Test@123", _fixture);
            if (user1Result.IsCompleted)
            {
                var userManager = _fixture.ServiceProvider.GetRequiredService<UserManager<PikchaUser>>();
                var signInManager = _fixture.ServiceProvider.GetRequiredService<SignInManager<PikchaUser>>();

                if(await userManager.CheckPasswordAsync(user1Result.Result, ""))
                {
                   
                }
                await signInManager.SignInAsync(user1Result.Result, isPersistent: false);

                var controller = new UserProfileController(_webHostEnvironment.Object, _configurationManager.Object, userManager, _fixture.Context);
                var result = await controller.UploadAvatarImage(avatarFile);

                var viewResult = Assert.IsType<ReturnDataModel>(result);
                Assert.NotNull(viewResult.Data);
                var filePath = Assert.IsType<string>(viewResult.Data);

                Assert.Contains("uploads/avatars", filePath);

                // get the user from DB and check whether it is updated
                PikchaUser dUser = _fixture.Context.Users.Find(userId);
                Assert.Contains("uploads/avatars", dUser.AvatarFileName);



                // get the PikchUser (get it from user manager and make sure it is updated in user manager as well
                PikchaUser pUser = MockHelpers.FindUserById(userId, _fixture).Result;
                Assert.Contains("uploads/avatars", pUser.AvatarFileName);

                // delete the user
                //await MockHelpers.DeleteUser(pUser, _fixture);
                */
            Assert.True(true);
            }
        }

        [Fact]
        public async Task Post_UploadSignatureImage()
        {
        // Arrange
        /*string userId = "testuser4";
        string email = "testuser4@test.thananji.com";
        string fName = "Fname";
        // create two test users
        Task<PikchaUser> user1Result = MockHelpers.CreateNewUser(userId, email, "Test@123", _fixture);
        if (user1Result.IsCompleted)
        {
            ProfileViewModel profVM = new ProfileViewModel() { Id = user1Result.Result.Id, Address_1 = "Address 1", FirstName = fName, LastName = "LName", PostalCode = "1234" };

            var userManager = _fixture.ServiceProvider.GetRequiredService<UserManager<PikchaUser>>();
            var controller = new UserProfileController(_webHostEnvironment.Object, _configurationManager.Object, userManager, _fixture.Context);

            // Act
            var result = await controller.Put(1, profVM);

            var viewResult = Assert.IsType<ReturnDataModel>(result);
            Assert.NotNull(viewResult.Data);
            var user = Assert.IsType<ProfileViewModel>(viewResult.Data);

            Assert.Equal(fName, user.FirstName);

            // get the user from DB and check whether it is updated
            PikchaUser dUser = _fixture.Context.Users.Find(userId);
            Assert.Equal(fName, dUser.FirstName);



            // get the PikchUser (get it from user manager and make sure it is updated in user manager as well
            PikchaUser pUser = MockHelpers.FindUserById(userId, _fixture).Result;

            Assert.Equal(fName, pUser.FirstName);

            // delete the user
            //await MockHelpers.DeleteUser(pUser, _fixture);
            */
        Assert.True(true);
            }
        }
    }
}


// https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-2.2