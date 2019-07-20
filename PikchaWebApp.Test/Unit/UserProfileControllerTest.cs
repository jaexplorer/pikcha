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

namespace PikchaWebApp.Test.Unit
{
    public class UserProfileControllerTest
    {

        [Fact]
        public async Task Index_ReturnsJsonAListofUsers()
        {
            // Arrange
            var mockRepoWebHost = new Mock<IWebHostEnvironment>();
            var mockRepoConfig = new Mock<IConfiguration>();
            var mockRepoUserMan = new Mock<UserManager<PikchaUser>>();
            var mockRepoDB = new Mock<PikchaDbContext>();

            var controller = new UserProfileController(mockRepoWebHost.Object, mockRepoConfig.Object, null, mockRepoDB.Object);

            // Act
            var result = await controller.Get();

            // Assert
            var viewResult = Assert.IsType<JsonResult>(result);
            var model = Assert.IsAssignableFrom<List<PikchaUser>>(
                viewResult.Value);
            Assert.Equal(2, model.Count);
            //Assert.Equal(2, 2);

        }

    }
}

// https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-2.2