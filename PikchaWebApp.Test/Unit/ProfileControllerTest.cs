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
                var result = await profContr.GetArtist(Guid.NewGuid().ToString()) as ObjectResult;

                Assert.Equal(statusCode, result.StatusCode);
                return;
            }

            var result2 = await profContr.GetArtist(pkUser.Id) as ObjectResult;

            Assert.Equal(statusCode, result2.StatusCode);

            var qUsr = result2.Value as ArtistDTO;
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
                 Bio = "UpdatedBio-Info",
                  FName = updateFName
            };
            var result = await profContr.UpdateUser(pkUser.Id, updtModel) as ObjectResult;

            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);

            // get the user from DB
            var updUsr = await _fixture.Context.PikchaUsers.FindAsync(pkUser.Id);

            Assert.Equal(updateFName, updUsr.FName);

        }

        [Fact]
        public async Task Update_Links_Success()
        {
            // create a new user
            string userId = Guid.NewGuid().ToString();
            var pkUser = await MockHelpers.CreateNewUser(1, userId, "test1@test.com", "Password@123", _fixture);

            // get user manager
            var profContr = CreateAuthenticatedProfileController(pkUser);


            LinkVM updtModel = new LinkVM()
            {
                 Type = "Facebook",
                 Url = "https://facebook.com/"
            };
            var result = await profContr.UpdateLinks(pkUser.Id, updtModel) as ObjectResult;
            var qUsr = result.Value as ArtistDTO;

            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.True(qUsr.Links.ContainsKey(updtModel.Type));
            Assert.True(qUsr.Links.ContainsValue(updtModel.Url));


            LinkVM updtModel2 = new LinkVM()
            {
                Type = "Facebook",
                Url = "https://facebook.com/2"
            };
            var result2 = await profContr.UpdateLinks(pkUser.Id, updtModel2) as ObjectResult;
            var qUsr2 = result2.Value as ArtistDTO;

            Assert.True(qUsr2.Links.ContainsValue(updtModel2.Url));

            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }


        // NEED to convert an image into Base64 and send it inside the bodydata
        [Theory]
        [InlineData("valid", StatusCodes.Status200OK)]
        //[InlineData("invalid", StatusCodes.Status500InternalServerError)]
        public async Task<PikchaUser> Post_Promote_Photo_Grapher_Test(string idType, int statusCode)
        {
            // create a new user
            string userId = Guid.NewGuid().ToString();
            var pkUser = await MockHelpers.CreateNewUser(31, userId, "test31@test.com", "Password@123", _fixture);

            var profCntrl = CreateAuthenticatedProfileController(pkUser);

            // create a photo grapaher role
            var roleManager = _fixture.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var role = new IdentityRole();
            //role.Id = Guid.NewGuid().ToString();
            role.Name = PikchaConstants.PIKCHA_ROLES_ARTIST_NAME;
            var s = roleManager.CreateAsync(role).Result;

            // create a file
            string imagePath = "wwwroot/TestPhotos/black-white.jpg";
            var img = MockHelpers.CreateImage(DateTime.Now.ToShortDateString(), "Caption 1", "location 1", imagePath);

            if (idType == "invalid")
            {
                using (var ms = new MemoryStream())
                {
                    img.ImageFile.CopyTo(ms);
                    //var fileBytes = ms.ToArray();
                    //string imgContent = Convert.ToBase64String(fileBytes);
                    profCntrl.HttpContext.Request.Body = ms; ;

                    var result2 = await profCntrl.PromoteUserToArtist(Guid.NewGuid().ToString()) as ObjectResult;
                    Assert.Equal(statusCode, result2.StatusCode);

                    return pkUser;
                }

            }
            using (var ms = new MemoryStream())
            {
                //ms.Position = 0;

                img.ImageFile.CopyTo(ms);
                var fileBytes = ms.ToArray();
                string imgContent = Convert.ToBase64String(fileBytes);

                var json = "{ \"signatureContent\": \""+ imgContent +"\"}";
                var bytes = System.Text.Encoding.UTF8.GetBytes(json.ToCharArray());
                var stream = new MemoryStream(bytes);
                
                //ms.Position = 0;
                profCntrl.HttpContext.Request.Body = stream;

                // act on the Base64 data
                var result = await profCntrl.PromoteUserToArtist(pkUser.Id) as ObjectResult;
                Assert.Equal(statusCode, result.StatusCode);

                var qUsr = result.Value as AuthenticatedUserDTO;

                Assert.Contains(PikchaConstants.PIKCHA_ROLES_ARTIST_NAME, qUsr.Roles);

                return pkUser;
            }

        }

        [Theory]
        [InlineData("valid", StatusCodes.Status200OK)]
        public async Task Post_UploadAvatarImage(string idType, int statusCode)
        {
            // TODO : Add login function
            // Arrange
             var configurationSection = new Mock<IConfigurationSection>();

            configurationSection.Setup(a => a.Value).Returns("uploads/avatars");
            _configurationManager.Setup(a => a.GetSection("UploadDirectories:Avatar")).Returns(configurationSection.Object);

            // create a new user
            string userId = Guid.NewGuid().ToString();
            var pkUser = await MockHelpers.CreateNewUser(51, userId, "test51@test.com", "Password@123", _fixture);
            var profCntrl = CreateAuthenticatedProfileController(pkUser);

            // create a file
            string imagePath = "wwwroot/TestPhotos/black-white.jpg";
            var img = MockHelpers.CreateImage(DateTime.Now.ToShortDateString(), "Caption 1", "location 1", imagePath);

            using (var ms = new MemoryStream())
            {
                //ms.Position = 0;

                img.ImageFile.CopyTo(ms);
                var fileBytes = ms.ToArray();
                string imgContent = Convert.ToBase64String(fileBytes);

                var json = "{ \"avatarContent\": \"" + imgContent + "\"}";
                var bytes = System.Text.Encoding.UTF8.GetBytes(json.ToCharArray());
                var stream = new MemoryStream(bytes);

                //ms.Position = 0;
                profCntrl.HttpContext.Request.Body = stream;

                // act on the Base64 data
                var result = await profCntrl.UploadAvatarImage(pkUser.Id) as ObjectResult;
                Assert.Equal(statusCode, result.StatusCode);

                var qUsr = result.Value as AuthenticatedUserDTO;

                Assert.True(File.Exists( PikchaConstants.PIKCHA_IMAGE_UPLOAD_ROOT_FOLDER + qUsr.Avatar));
                    

            }
        }

        [Theory]
        [InlineData("valid", StatusCodes.Status200OK)]
        public async Task Post_UploadCoverImage(string idType, int statusCode)
        {
            // TODO : Add login function
            // Arrange
            var configurationSection = new Mock<IConfigurationSection>();

            configurationSection.Setup(a => a.Value).Returns("uploads/covers");
            _configurationManager.Setup(a => a.GetSection("UploadDirectories:Cover")).Returns(configurationSection.Object);

           // create an artist
            var artist = await Post_Promote_Photo_Grapher_Test("valid", 200);
          
            var profCntrl = CreateAuthenticatedProfileController(artist);

            // create a file
            string imagePath = "wwwroot/TestPhotos/black-white.jpg";
            var img = MockHelpers.CreateImage(DateTime.Now.ToShortDateString(), "Caption 1", "location 1", imagePath);

            using (var ms = new MemoryStream())
            {
                //ms.Position = 0;

                img.ImageFile.CopyTo(ms);
                var fileBytes = ms.ToArray();
                string imgContent = Convert.ToBase64String(fileBytes);

                var json = "{ \"coverContent\": \"" + imgContent + "\"}";
                var bytes = System.Text.Encoding.UTF8.GetBytes(json.ToCharArray());
                var stream = new MemoryStream(bytes);

                //ms.Position = 0;
                profCntrl.HttpContext.Request.Body = stream;

                // act on the Base64 data
                var result = await profCntrl.UploadCoverImage(artist.Id) as ObjectResult;
                Assert.Equal(statusCode, result.StatusCode);

                var qUsr = result.Value as ArtistDTO;

                Assert.True(File.Exists(PikchaConstants.PIKCHA_IMAGE_UPLOAD_ROOT_FOLDER + qUsr.Avatar));


            }
        }


        // NEED to convert an image into Base64 and send it inside the bodydata for uploading signature

        [Fact]
        [InlineData("invalid", StatusCodes.Status500InternalServerError)]
        public async Task Get_Signature()
        {
            var artist = await Post_Promote_Photo_Grapher_Test("valid", 200);

            var profCntrl = CreateAuthenticatedProfileController(artist);

            var result = await profCntrl.GetSignature(artist.Id) as ObjectResult;
            Assert.Equal(200, result.StatusCode);

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

            var qUsr = result.Value as AuthenticatedUserDTO;


            Assert.True(qUsr.Following.Count > 0);
            Assert.True(qUsr.Following.Find(u => u.Id == pkArtist.Id) != null);
                        

            // check the artist profile for following 

            var profArtGetRes = await profCntrl.GetArtist(pkArtist.Id) as ObjectResult;
            var prfUsr = profArtGetRes.Value as ArtistDTO;

            Assert.True(prfUsr.Followers.Count > 0);
            Assert.True(prfUsr.Followers.Find(u => u.Id == qUsr.Id) != null);
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

            var qUsr = fresult.Value as AuthenticatedUserDTO;

            Assert.Equal(200, fresult.StatusCode);
            Assert.True(qUsr.Following.Find(u => u.Id == pkArtist.Id) != null);

            if (idType == "invalid")
            {
                var result2 = await profCntrl.UnFollowAnArtist(Guid.NewGuid().ToString(), pkUser.Id) as ObjectResult;
                Assert.Equal(statusCode, result2.StatusCode);
                return;
            }

            var result = await profCntrl.UnFollowAnArtist(pkArtist.Id, pkUser.Id) as ObjectResult;
            Assert.Equal(statusCode, result.StatusCode);
            
            
            var qUsr2 = result.Value as AuthenticatedUserDTO;

            Assert.True(qUsr2.Following.Find(u => u.Id == pkArtist.Id) == null);
            //var qUsr = result.Value as PikchaAuthenticatedUserDTO;

            //Assert.True(File.Exists(qUsr.Avatar));

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
        //          var result = await controller.GetArtist(user1Result.Result.Id);

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
        //          ProfileViewModel profVM = new ProfileViewModel() { PerAddress1 = "Address 1", FName = fName, LName = "LName", PerPostalCode = "1234" };

        //         /* var userManager = _fixture.ServiceProvider.GetRequiredService<UserManager<PikchaUser>>();
        //          var controller = new ProfileController(_webHostEnvironment.Object, _configurationManager.Object, userManager, _fixture.Context);

        //          // Act
        //          var result = await controller.UpdateUser(1, profVM);

        //          var viewResult = Assert.IsType<ReturnDataModel>(result);
        //          Assert.NotNull(viewResult.Data);
        //          var user = Assert.IsType<ProfileViewModel>(viewResult.Data);

        //          Assert.Equal(fName, user.FName);

        //          // get the user from DB and check whether it is updated
        //          PikchaUser dUser = _fixture.Context.Users.Find(userId);
        //          Assert.Equal(fName, dUser.FName);
        //          */


        //          // get the PikchUser (get it from user manager and make sure it is updated in user manager as well
        //          PikchaUser pUser = MockHelpers.FindUserById(userId, _fixture).Result;

        //          Assert.Equal(fName, pUser.FName);

        //          // delete the user
        //          await MockHelpers.DeleteUser(pUser, _fixture);
        //      }
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
        //          ProfileViewModel profVM = new ProfileViewModel() { Id = user1Result.Result.Id, Address_1 = "Address 1", FName = fName, LName = "LName", Postal = "1234" };

        //          var userManager = _fixture.ServiceProvider.GetRequiredService<UserManager<PikchaUser>>();
        //          var controller = new UserProfileController(_webHostEnvironment.Object, _configurationManager.Object, userManager, _fixture.Context);

        //          // Act
        //          var result = await controller.UpdateUser(1, profVM);

        //          var viewResult = Assert.IsType<ReturnDataModel>(result);
        //          Assert.NotNull(viewResult.Data);
        //          var user = Assert.IsType<ProfileViewModel>(viewResult.Data);

        //          Assert.Equal(fName, user.FName);

        //          // get the user from DB and check whether it is updated
        //          PikchaUser dUser = _fixture.Context.Users.Find(userId);
        //          Assert.Equal(fName, dUser.FName);



        //          // get the PikchUser (get it from user manager and make sure it is updated in user manager as well
        //          PikchaUser pUser = MockHelpers.FindUserById(userId, _fixture).Result;

        //          Assert.Equal(fName, pUser.FName);

        //          // delete the user
        //          //await MockHelpers.DeleteUser(pUser, _fixture);
        //          */
        //      Assert.True(true);
        //  }

    }
}


// https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-2.2