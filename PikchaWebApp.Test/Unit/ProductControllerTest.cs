using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using PikchaWebApp.Data;
using PikchaWebApp.Test.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PikchaWebApp.Test.Unit
{
    public class ProductControllerTest : IClassFixture<SqliteInMemoryFixture>
    {
        private readonly SqliteInMemoryFixture _fixture;
        private readonly Mock<IWebHostEnvironment> _webHostEnvironment;
        private readonly IMapper _imapper;
        private readonly Mock<IConfiguration> _configurationManager;

        public ProductControllerTest(SqliteInMemoryFixture fixture)
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

        public async Task Get_ImageFromProduct(string type, int start, int count, int expectedStatus)
        {
            // TO DO
            Assert.True(true);
        }
    }
}
