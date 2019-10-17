using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using PikchaWebApp.Test.Shared;
using PikchaWebApp.Controllers;
using PikchaWebApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using PikchaWebApp.Data;
using System.IO;
using PikchaWebApp.Managers;

namespace PikchaWebApp.Test.Unit
{
    public class ProfileControllerTest : IClassFixture<SqliteInMemoryFixture>
    {

        protected readonly UserManager<PikchaUser> _userManager;
        private readonly SqliteInMemoryFixture _fixture;
        private readonly Mock<IWebHostEnvironment> _webHostEnvironment;
        private readonly Mock<IConfiguration> _configurationManager;
        private readonly IMapper _imapper;
        public ProfileControllerTest(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.CreateDatabase();
            _webHostEnvironment = new Mock<IWebHostEnvironment>();
            _configurationManager = new Mock<IConfiguration>();

            _webHostEnvironment.Setup(repo => repo.WebRootPath).Returns(".");

            var myProfile = new PikchaDTOProfiles();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _imapper = new Mapper(configuration);
        }

        [Theory]
        [InlineData("valid", StatusCodes.Status200OK)]
        //[InlineData("invalid", StatusCodes.Status500InternalServerError)]
        public async Task Get_User_Test(string idType, int statusCode)
        {
            // create a new user
            string userId = Guid.NewGuid().ToString();
            var pkUser = await MockHelpers.CreateNewUser(1, userId, "test1@test.com", "Password@123", _fixture);
                       
            // get user manager
            var profContr = CreateAuthenticatedProfileController(pkUser);


            if (idType == "invalid")
            {
                var result = await profContr.GetUser(Guid.NewGuid().ToString()) as ObjectResult;

                Assert.Equal(statusCode, result.StatusCode);
                return;
            }

            var result2 = await profContr.GetUser(pkUser.Id) as ObjectResult;

            Assert.Equal(statusCode, result2.StatusCode);

            var qUsr = result2.Value as Pikcha100ArtistDTO;
            Assert.Equal(userId, qUsr.Id);
            return;

        }


        [Fact]
        public async Task Update_User_Success()
        {
            // create a new user
            string userId = Guid.NewGuid().ToString();
            var pkUser = await MockHelpers.CreateNewUser(1, userId, "test1@test.com", "Password@123", _fixture);
            
            // get user manager
            var profContr = CreateAuthenticatedProfileController(pkUser);


            string updateFName = "Updated First Name2";
            ProfileViewModel updtModel = new ProfileViewModel()
            {
                 BioInfo = "UpdatedBio-Info",
                  FirstName = updateFName
            };
            var result = await profContr.UpdateUser(pkUser.Id, updtModel) as ObjectResult;

            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);

            // get the user from DB
            var updUsr = await _fixture.Context.PikchaUsers.FindAsync(pkUser.Id);

            Assert.Equal(updateFName, updUsr.FirstName);

        }

        [Theory]
        [InlineData("valid", StatusCodes.Status200OK)]
        //[InlineData("invalid", StatusCodes.Status500InternalServerError)]
        public async Task Post_Promote_Photo_Grapher_Test(string idType, int statusCode)
        {
            // create a new user
            string userId = Guid.NewGuid().ToString();
            var pkUser = await MockHelpers.CreateNewUser(31, userId, "test31@test.com", "Password@123", _fixture);

            var profCntrl = CreateAuthenticatedProfileController(pkUser);

            // create a photo grapaher role
            var roleManager = _fixture.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var role = new IdentityRole();
            //role.Id = Guid.NewGuid().ToString();
            role.Name = PikchaConstants.PIKCHA_ROLES_PHOTOGRAPHER_NAME;
            var s = roleManager.CreateAsync(role).Result;

            // create a file
            var img = MockHelpers.CreateImage(DateTime.Now.ToShortDateString(), "Caption 1", "location 1");

            if(idType == "invalid")
            {
                var result2 = await profCntrl.PromoteUserToPhotographer(Guid.NewGuid().ToString(), img.ImageFile) as ObjectResult;
                Assert.Equal(statusCode, result2.StatusCode);

                return;
            }

            var result = await profCntrl.PromoteUserToPhotographer(pkUser.Id, img.ImageFile) as ObjectResult;
            Assert.Equal(statusCode, result.StatusCode);

            var qUsr = result.Value as PikchaAuthenticatedUserDTO;

            Assert.Contains(PikchaConstants.PIKCHA_ROLES_PHOTOGRAPHER_NAME, qUsr.Roles);

        }


        [Theory]
        [InlineData("valid", StatusCodes.Status200OK)]
        //[InlineData("invalid", StatusCodes.Status500InternalServerError)]
        public async Task Post_Upload_Avatar_Image_Test(string idType, int statusCode)
        {
            // create a new user
            string userId = Guid.NewGuid().ToString();
            var pkUser = await MockHelpers.CreateNewUser(41, userId, "test41@test.com", "Password@123", _fixture);

            var profCntrl = CreateAuthenticatedProfileController(pkUser);

            // create a photo grapaher role

            // create a file
            var img = MockHelpers.CreateImage(DateTime.Now.ToShortDateString(), "Caption 1", "location 1");

            if (idType == "invalid")
            {
                var result2 = await profCntrl.UploadAvatarImage(Guid.NewGuid().ToString(), img.ImageFile) as ObjectResult;
                Assert.Equal(statusCode, result2.StatusCode);

                return;
            }

            var result = await profCntrl.UploadAvatarImage(pkUser.Id, img.ImageFile) as ObjectResult;
            Assert.Equal(statusCode, result.StatusCode);

            var qUsr = result.Value as PikchaAuthenticatedUserDTO;

            Assert.True(File.Exists(qUsr.AvatarFileName));

        }


        [Theory]
        [InlineData("valid", StatusCodes.Status200OK)]
        [InlineData("invalid", StatusCodes.Status404NotFound)]
        public async Task Post_Follow_Artist_Test(string idType, int statusCode)
        {
            // create a new user
            string userId = Guid.NewGuid().ToString();
            var pkUser = await MockHelpers.CreateNewUser(51, userId, "test51@test.com", "Password@123", _fixture);

            string artistId = Guid.NewGuid().ToString();

            var pkArtist = await MockHelpers.CreateNewUser(61, artistId, "test61@test.com", "Password@123", _fixture);

            var profCntrl = CreateAuthenticatedProfileController(pkUser);
            
            if (idType == "invalid")
            {
                var result2 = await profCntrl.FollowAnArtist(Guid.NewGuid().ToString(), pkUser.Id) as ObjectResult;
                Assert.Equal(statusCode, result2.StatusCode);
                return;
            }

            var result = await profCntrl.FollowAnArtist(pkArtist.Id, pkUser.Id) as ObjectResult;
            Assert.Equal(statusCode, result.StatusCode);

            var qUsr = result.Value as PikchaAuthenticatedUserDTO;


            Assert.True(qUsr.Following.Count > 0);

            //var qUsr = result.Value as PikchaAuthenticatedUserDTO;

            //Assert.True(File.Exists(qUsr.AvatarFileName));

        }

        [Theory]
        [InlineData("valid", StatusCodes.Status200OK)]
        //[InlineData("invalid", StatusCodes.Status404NotFound)]
        public async Task Post_UnFollow_Artist_Test(string idType, int statusCode)
        {
            // create a new user
            string userId = Guid.NewGuid().ToString();
            var pkUser = await MockHelpers.CreateNewUser(51, userId, "test51@test.com", "Password@123", _fixture);

            string artistId = Guid.NewGuid().ToString();

            var pkArtist = await MockHelpers.CreateNewUser(61, artistId, "test61@test.com", "Password@123", _fixture);
            
            var profCntrl = CreateAuthenticatedProfileController(pkUser);
            var fresult = await profCntrl.FollowAnArtist(pkArtist.Id, pkUser.Id) as ObjectResult;

            Assert.Equal(200, fresult.StatusCode);

            if (idType == "invalid")
            {
                var result2 = await profCntrl.UnFollowAnArtist(Guid.NewGuid().ToString(), pkUser.Id) as ObjectResult;
                Assert.Equal(statusCode, result2.StatusCode);
                return;
            }

            var result = await profCntrl.UnFollowAnArtist(pkArtist.Id, pkUser.Id) as ObjectResult;
            Assert.Equal(statusCode, result.StatusCode);

            //var qUsr = result.Value as PikchaAuthenticatedUserDTO;

            //Assert.True(File.Exists(qUsr.AvatarFileName));

        }



        private ProfileController CreateAuthenticatedProfileController(PikchaUser pkUser)
        {
            // get user manager
            var _userManager = MockHelpers.CreateMockUserManager(pkUser);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, pkUser.Id)
            }));
            var profContr = new ProfileController(_webHostEnvironment.Object, _configurationManager.Object, _userManager, _fixture.Context, _imapper);
            profContr.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            return profContr;
        }


        //  [Fact]
        //  public async Task Get_ReturnsJson_ListofUsers()
        //  {
        //      // Arrange            
        //      // create two test users
        //      Task<PikchaUser> user1Result = MockHelpers.CreateNewUser(1, "testuser1", "testuser1@test.thananji.com", "Test@123", _fixture);
        //      Task<PikchaUser> user2Result = MockHelpers.CreateNewUser(2, "testuser2", "testuser2@test.thananji.com", "Test@123", _fixture);
        //      Task.WaitAll(user1Result, user2Result);

        //      _imapper.Setup(x => x.Map<PikchaRandomImageDTO>(It.IsAny<PikchaImage>()))
        //.Returns((PikchaImage source) => new PikchaRandomImageDTO() { });

        //      var userManager = _fixture.ServiceProvider.GetRequiredService<UserManager<PikchaUser>>();
        //      var controller = new ProfileController(_webHostEnvironment.Object, _configurationManager.Object, userManager, _fixture.Context, _imapper.Object);

        //      // Act
        //      var result = await controller.List();

        //      // Assert
        //      /*var viewResult = Assert.IsType<JsonResult>(result);
        //      var model = Assert.IsAssignableFrom<List<PikchaUser>>(
        //          viewResult.Value);
        //      Assert.Equal(2, model.Count); */


        //      var viewResult = Assert.IsType<JsonResult>(result);
        //      Assert.Equal(200, viewResult.StatusCode);

        //      var users = Assert.IsType<List<PikchaRandomImageDTO>>(viewResult.Value);

        //      Assert.Equal(2, users.Count);

        //      // delete the user
        //      await MockHelpers.DeleteUser(user1Result.Result, _fixture);
        //      await MockHelpers.DeleteUser(user2Result.Result, _fixture); 
        //  }

        //  [Fact]
        //  public async Task Get_ReturnAUser()
        //  {
        //      // Arrange
        //      string email = "testuser3@test.thananji.com";
        //      // create two test users
        //      Task<PikchaUser> user1Result = MockHelpers.CreateNewUser(1, "testuser1", email, "Test@123", _fixture);
        //      if (user1Result.IsCompleted)
        //      {
        //          /*
        //          var userManager = _fixture.ServiceProvider.GetRequiredService<UserManager<PikchaUser>>();
        //          var controller = new ProfileController(_webHostEnvironment.Object, _configurationManager.Object, userManager, _fixture.Context);

        //          // Act
        //          var result = await controller.GetUser(user1Result.Result.Id);

        //          var viewResult = Assert.IsType<ReturnDataModel>(result);
        //          Assert.NotNull(viewResult.Data);
        //          var user = Assert.IsType<PikchaUser>(viewResult.Data);

        //          Assert.Equal(email, user.Email);
        //          // delete the user
        //          await MockHelpers.DeleteUser(user, _fixture); */
        //      }
        //  }

        //  [Fact]
        //  public async Task Put_UpdateAUser()
        //  {
        //      // Arrange
        //      string userId = "testuser4";
        //      string email = "testuser4@test.thananji.com";
        //      string fName = "Fname";
        //      // create two test users
        //      Task<PikchaUser> user1Result = MockHelpers.CreateNewUser(1, userId, email, "Test@123", _fixture);
        //      if (user1Result.IsCompleted)
        //      {
        //          ProfileViewModel profVM = new ProfileViewModel() { PerAddress1 = "Address 1", FirstName = fName, LastName = "LName", PerPostalCode = "1234" };

        //         /* var userManager = _fixture.ServiceProvider.GetRequiredService<UserManager<PikchaUser>>();
        //          var controller = new ProfileController(_webHostEnvironment.Object, _configurationManager.Object, userManager, _fixture.Context);

        //          // Act
        //          var result = await controller.UpdateUser(1, profVM);

        //          var viewResult = Assert.IsType<ReturnDataModel>(result);
        //          Assert.NotNull(viewResult.Data);
        //          var user = Assert.IsType<ProfileViewModel>(viewResult.Data);

        //          Assert.Equal(fName, user.FirstName);

        //          // get the user from DB and check whether it is updated
        //          PikchaUser dUser = _fixture.Context.Users.Find(userId);
        //          Assert.Equal(fName, dUser.FirstName);
        //          */


        //          // get the PikchUser (get it from user manager and make sure it is updated in user manager as well
        //          PikchaUser pUser = MockHelpers.FindUserById(userId, _fixture).Result;

        //          Assert.Equal(fName, pUser.FirstName);

        //          // delete the user
        //          await MockHelpers.DeleteUser(pUser, _fixture);
        //      }
        //  }


        //  [Fact]
        //  public async Task Post_UploadAvatarImage()
        //  {
        //      // TODO : Add login function
        //      // Arrange
        //      /* var configurationSection = new Mock<IConfigurationSection>();

        //      configurationSection.Setup(a => a.Value).Returns("uploads/avatars");
        //      _configurationManager.Setup(a => a.GetSection("UploadDirectories.Avatar")).Returns(configurationSection.Object);

        //      IFormFile avatarFile = MockHelpers.CreateNewImageFile("TestPhotos/profile-photo.jpg", "profile-photo.jpg", "profile-photo");

        //      string userId = "testuser4";
        //      string email = "testuser4@test.thananji.com";
        //      // create two test users
        //      Task<PikchaUser> user1Result = MockHelpers.CreateNewUser(userId, email, "Test@123", _fixture);
        //      if (user1Result.IsCompleted)
        //      {
        //          var userManager = _fixture.ServiceProvider.GetRequiredService<UserManager<PikchaUser>>();
        //          var signInManager = _fixture.ServiceProvider.GetRequiredService<SignInManager<PikchaUser>>();

        //          if(await userManager.CheckPasswordAsync(user1Result.Result, ""))
        //          {

        //          }
        //          await signInManager.SignInAsync(user1Result.Result, isPersistent: false);

        //          var controller = new UserProfileController(_webHostEnvironment.Object, _configurationManager.Object, userManager, _fixture.Context);
        //          var result = await controller.UploadAvatarImage(avatarFile);

        //          var viewResult = Assert.IsType<ReturnDataModel>(result);
        //          Assert.NotNull(viewResult.Data);
        //          var filePath = Assert.IsType<string>(viewResult.Data);

        //          Assert.Contains("uploads/avatars", filePath);

        //          // get the user from DB and check whether it is updated
        //          PikchaUser dUser = _fixture.Context.Users.Find(userId);
        //          Assert.Contains("uploads/avatars", dUser.AvatarFileName);



        //          // get the PikchUser (get it from user manager and make sure it is updated in user manager as well
        //          PikchaUser pUser = MockHelpers.FindUserById(userId, _fixture).Result;
        //          Assert.Contains("uploads/avatars", pUser.AvatarFileName);

        //          // delete the user
        //          //await MockHelpers.DeleteUser(pUser, _fixture);
        //          */
        //      Assert.True(true);
        //  }


        //  [Fact]
        //  public async Task Post_UploadSignatureImage()
        //  {
        //      // Arrange
        //      /*string userId = "testuser4";
        //      string email = "testuser4@test.thananji.com";
        //      string fName = "Fname";
        //      // create two test users
        //      Task<PikchaUser> user1Result = MockHelpers.CreateNewUser(userId, email, "Test@123", _fixture);
        //      if (user1Result.IsCompleted)
        //      {
        //          ProfileViewModel profVM = new ProfileViewModel() { Id = user1Result.Result.Id, Address_1 = "Address 1", FirstName = fName, LastName = "LName", PostalCode = "1234" };

        //          var userManager = _fixture.ServiceProvider.GetRequiredService<UserManager<PikchaUser>>();
        //          var controller = new UserProfileController(_webHostEnvironment.Object, _configurationManager.Object, userManager, _fixture.Context);

        //          // Act
        //          var result = await controller.UpdateUser(1, profVM);

        //          var viewResult = Assert.IsType<ReturnDataModel>(result);
        //          Assert.NotNull(viewResult.Data);
        //          var user = Assert.IsType<ProfileViewModel>(viewResult.Data);

        //          Assert.Equal(fName, user.FirstName);

        //          // get the user from DB and check whether it is updated
        //          PikchaUser dUser = _fixture.Context.Users.Find(userId);
        //          Assert.Equal(fName, dUser.FirstName);



        //          // get the PikchUser (get it from user manager and make sure it is updated in user manager as well
        //          PikchaUser pUser = MockHelpers.FindUserById(userId, _fixture).Result;

        //          Assert.Equal(fName, pUser.FirstName);

        //          // delete the user
        //          //await MockHelpers.DeleteUser(pUser, _fixture);
        //          */
        //      Assert.True(true);
        //  }

    }
}


// https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-2.2