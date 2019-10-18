using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PikchaWebApp.Controllers;
using PikchaWebApp.Data;
using PikchaWebApp.Models;
using PikchaWebApp.Test.Shared;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Linq;

namespace PikchaWebApp.Test.Unit
{
    public class ImageControllerTest : IClassFixture<SqliteInMemoryFixture>
    {
        protected readonly UserManager<PikchaUser> _userManager;
        private readonly SqliteInMemoryFixture _fixture;
        private readonly Mock<IWebHostEnvironment> _webHostEnvironment;
        private readonly Mock<IConfiguration> _configurationManager;
        private readonly IMapper _imapper;

        public ImageControllerTest(SqliteInMemoryFixture fixture)
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
        [Fact]
        public async Task UploadImage_Success()
        {

            // create image
            var img = MockHelpers.CreateImage(DateTime.Now.ToShortDateString(), "Caption 1", "location 1");

            // create a new user
            var pkUser = await MockHelpers.CreateNewUser(1, Guid.NewGuid().ToString(), "test1@test.com", "Password@123", _fixture);

            // get user manager
            var _userManager = MockHelpers.CreateMockUserManager(pkUser);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, pkUser.Id)//,
                //new Claim(ClaimTypes.Name, pkUser.UserName),
               // new Claim(ClaimTypes.Email, pkUser.Email),
                //new Claim(ClaimTypes.Role, "Admin")
            }));

            // upload image
            var imgCont = new ImageController(_webHostEnvironment.Object, _configurationManager.Object, _userManager, _fixture.Context, _imapper);

            imgCont.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
            var response = await imgCont.UploadImage(img) as StatusCodeResult;

            Assert.Equal(201, response.StatusCode); //success

        }
    
        [Theory]
        [InlineData("valid", StatusCodes.Status200OK)]
        [InlineData("invalid", StatusCodes.Status500InternalServerError)]

        public async Task Get_ImageByID(string idType, int statusCode)
        {
            // create image
            var img = MockHelpers.CreateImage(DateTime.Now.ToShortDateString(), "Caption 1", "location 1");

            // create a new user
            var pkUser = await MockHelpers.CreateNewUser(1, Guid.NewGuid().ToString(), "test1@test.com", "Password@123", _fixture);

            // get user manager
            var _userManager = MockHelpers.CreateMockUserManager(pkUser);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, pkUser.Id)//,
                //new Claim(ClaimTypes.Name, pkUser.UserName),
               // new Claim(ClaimTypes.Email, pkUser.Email),
                //new Claim(ClaimTypes.Role, "Admin")
            }));

            // upload image
            var imgCont = new ImageController(_webHostEnvironment.Object, _configurationManager.Object, _userManager, _fixture.Context, _imapper);

            imgCont.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
            ActionResult upRes = await imgCont.UploadImage(img);


            var imgContr = new ImageController(_webHostEnvironment.Object, _configurationManager.Object, _userManager, _fixture.Context, _imapper);

            if(idType == "invalid")
            {
                var response = await imgContr.GetById(Guid.NewGuid().ToString()) as ObjectResult;
                Assert.Equal(statusCode, response.StatusCode);
                return;
            }
            // upload an image

            

            var filtContr = new FilterController(_fixture.Context, _imapper);

            var responseV = await filtContr.Images() as ObjectResult;
            var imlst = responseV.Value as List<PikchaRandomImageDTO>;

            var responseS = await imgContr.GetById(imlst[0].PikchaImageId) as ObjectResult;
            Assert.Equal(statusCode, responseS.StatusCode);

        }



        [Fact]
        public async Task Post_IncrementViewCount()
        {
            var img = MockHelpers.CreateImage(DateTime.Now.ToShortDateString(), "Caption 1", "location 1");
            // create a new user
            var pkUser = await MockHelpers.CreateNewUser(41, Guid.NewGuid().ToString(), "test1@test.com", "Password@123", _fixture);

            var authImgContr = CreateAuthenticatedImageController(pkUser);

            ActionResult upRes = await authImgContr.UploadImage(img);

            var imgContr = new ImageController(_webHostEnvironment.Object, _configurationManager.Object, _userManager, _fixture.Context, _imapper);

            var pkImg =  _fixture.Context.PikchaImages.First();
            
            var responseV = await imgContr.IncrementViewCount(pkImg.PikchaImageId) as StatusCodeResult;

            Assert.Equal(201, responseV.StatusCode);

            Assert.True(pkImg.PikchaImageViews.Count() > 0);

        }


        private ImageController CreateAuthenticatedImageController(PikchaUser pkUser)
        {
            // get user manager
            var _userManager = MockHelpers.CreateMockUserManager(pkUser);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, pkUser.Id)
            }));
            var imgfContr = new ImageController(_webHostEnvironment.Object, _configurationManager.Object, _userManager, _fixture.Context, _imapper);
            imgfContr.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            return imgfContr;
        }


    }
}
