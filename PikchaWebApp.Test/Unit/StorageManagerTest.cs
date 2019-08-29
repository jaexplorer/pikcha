using Microsoft.AspNetCore.Hosting;
using Moq;
using PikchaWebApp.Managers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Configuration;
using PikchaWebApp.Test.Shared;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace PikchaWebApp.Test.Unit
{
    public class StorageManagerTest
    {
        [Fact]
        public async Task UploadToLocalDirectory_Success()
        {
            var mockRepoWebHost = new Mock<IWebHostEnvironment>();
            mockRepoWebHost.Setup(x => x.WebRootPath)
                  .Returns(@"C:\Users\tshanmuganat\Documents\Projects\pikcha\dev\PikchaWebApp\PikchaWebApp.Test");

            var mockRepoConfig = new Mock<IConfiguration>();
            var configurationSection = new Mock<IConfigurationSection>();
            configurationSection.Setup(a => a.Value).Returns("uploads/avatars");
            mockRepoConfig.Setup(a => a.GetSection("UploadDirectories.Avatar")).Returns(configurationSection.Object);

            StorageManager manager = new StorageManager(mockRepoWebHost.Object, mockRepoConfig.Object);

            IFormFile mockFile = MockHelpers.CreateNewImageFile("TestPhotos/profile-photo.jpg", "profile-photo.jpg", "profile-photo");

            string filePath = string.Empty;
            var fileUploadTask = manager.UploadToLocalDirectory(mockFile, StorageManager.FileCategory.Avatar);
            filePath = fileUploadTask.Result;
            Assert.True(File.Exists(filePath));

            Assert.Contains("uploads/avatars", filePath);
            //Assert.Equal(2, 3);
        }

    }
}

// https://dzone.com/articles/file-uploads-in-aspnet-core-integration-tests
