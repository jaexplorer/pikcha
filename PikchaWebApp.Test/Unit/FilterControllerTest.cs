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
using System.IO;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PikchaWebApp.Test.Unit
{
    public class FilterControllerTest : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly SqliteInMemoryFixture _fixture;
        private readonly Mock<IWebHostEnvironment> _webHostEnvironment;
        private readonly IMapper _imapper;
        private readonly Mock<IConfiguration> _configurationManager;
        public FilterControllerTest(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.CreateDatabase();
            //_imapper = new Mock<IMapper>();
            _webHostEnvironment = new Mock<IWebHostEnvironment>();
            _configurationManager = new Mock<IConfiguration>();

            _webHostEnvironment.Setup(repo => repo.WebRootPath).Returns(".");

            var myProfile = new PikchaDTOProfiles();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _imapper = new Mapper(configuration);
        }

        [Theory]
        [InlineData("random", 0, 2, StatusCodes.Status200OK)]
        [InlineData("random", 15, 2, StatusCodes.Status416RequestedRangeNotSatisfiable)]
        [InlineData("pikcha100", 0, 2, StatusCodes.Status200OK)]
        [InlineData("pikcha100", 10, 20, StatusCodes.Status416RequestedRangeNotSatisfiable)]
        [InlineData("artist100", 0, 2, StatusCodes.Status200OK)]
        [InlineData("artist100", 10, 20, StatusCodes.Status416RequestedRangeNotSatisfiable)]
        [InlineData("artistId", 0, 2, StatusCodes.Status200OK)]
        [InlineData("artistId", 10, 20, StatusCodes.Status416RequestedRangeNotSatisfiable)]

        public async Task Get_ReturnsJson_ListofImages(string type, int start, int count, int expectedStatus)
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
            
            // check uploaded images are there
            var filtContr = new FilterController(_fixture.Context, _imapper);
            ObjectResult response ;
            if(type == "artistId")
            {
                response = await filtContr.Images(type, start, count, pkUser.Id) as ObjectResult;
            }
            else
            {
                response = await filtContr.Images(type, start, count) as ObjectResult;

            }
            Assert.Equal(expectedStatus, response.StatusCode);
            if(expectedStatus != StatusCodes.Status200OK)
            {
                return;
            }
           
            //var okResult = Assert.IsType<OkObjectResult>(response);
            if (type == "pikcha100")
            {
                var imlst = response.Value as List<PikchaImageFilterDTO>;
                // validate uploaded image
                Assert.True(imlst.Exists(i => i.Title == img.Title));

                Assert.True(File.Exists(imlst[0].Thumbnail));
                Assert.True(File.Exists(imlst[0].Watermark));
                return;
            }

            var lst = response.Value as List<PikchaImageFilterDTO>;
            // validate uploaded image
            Assert.True(lst.Exists(i => i.Title == img.Title));

            Assert.True(File.Exists(lst[0].Thumbnail));
            Assert.True(File.Exists(lst[0].Watermark));

        }


        //[Theory]
        //[InlineData("random", 0, 2, StatusCodes.Status200OK)]
        //[InlineData("random", 15, 2, StatusCodes.Status416RequestedRangeNotSatisfiable)]
        //[InlineData("artists100", 0, 2, StatusCodes.Status200OK)]
        //[InlineData("artists100", 10, 20, StatusCodes.Status416RequestedRangeNotSatisfiable)]
        //public async Task Get_ReturnsJson_ListofArtists(string type, int start, int count, int expectedStatus)
        //{
        //    // create a new user
        //    string email1 = "artist1@test.com";
        //    string email2 = "artist2@test.com";
        //    var pkUser = await MockHelpers.CreateNewUser(21, Guid.NewGuid().ToString(), email1, "Password@123", _fixture);
        //    var pkUser2 = await MockHelpers.CreateNewUser(22, Guid.NewGuid().ToString(), email2, "Password@123", _fixture);
            
        //    // check uploaded images are there
        //    var filtContr = new FilterController(_fixture.Context, _imapper);

        //    var response = await filtContr.Artists(type, start, count) as ObjectResult;
        //    Assert.Equal(expectedStatus, response.StatusCode);
        //    if (expectedStatus != StatusCodes.Status200OK)
        //    {
        //        return;
        //    }

        //    //var okResult = Assert.IsType<OkObjectResult>(response);
        //    if (type == "artists100")
        //    {
        //        var imlst = response.Value as List<PikchaArtistDTO>;
        //        // validate uploaded image
        //        Assert.True(imlst.Exists(i => i.Email == email1));
        //        return;
        //    }

        //    var lst = response.Value as List<PikchaArtistDTO >;
        //    // validate uploaded image
        //    Assert.True(lst.Exists(i => i.Email == email2));            
        //}

    }
}
