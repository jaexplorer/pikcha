using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PikchaWebApp.Controllers;
using PikchaWebApp.Models;
using PikchaWebApp.Test.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PikchaWebApp.Test.Unit
{
    public class ImageControllerTest : IClassFixture<SqliteInMemoryFixture>
    {
        protected readonly UserManager<PikchaUser> _userManager;
        private readonly SqliteInMemoryFixture _fixture;
        Mock<IWebHostEnvironment> _webHostEnvironment;
        Mock<IConfiguration> _configurationManager;

        public ImageControllerTest(SqliteInMemoryFixture fixture)
        {
            _fixture = fixture;
            _fixture.CreateDatabase();
            _webHostEnvironment = new Mock<IWebHostEnvironment>();
            _configurationManager = new Mock<IConfiguration>();

            _webHostEnvironment.Setup(repo => repo.WebRootPath).Returns(".");
        }
        [Fact]
        public async Task UploadImage_Success()
        {


            IFormFile imgFile = MockHelpers.CreateNewImageFile("TestPhotos/beach-jaffna.jpg", "beach-jaffna.jpg", "beach-jaffna");

            ImageViewModel vmImage = new ImageViewModel();
            vmImage.Title = "Point Pedro Beach";
            vmImage.Caption = "Lovely sand beach";
            vmImage.Location = "Point Pedro";
            vmImage.NumberOfPrint = 10;
            vmImage.ImageFile = imgFile;

            var userManager = _fixture.ServiceProvider.GetRequiredService<UserManager<PikchaUser>>();

            var controller = new ImageController(_webHostEnvironment.Object, _configurationManager.Object, userManager, _fixture.Context);

            // Act
            var result = await controller.UploadImage(vmImage);

            Assert.Equal(200, result.Statuscode);

        }
    }
}
