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

namespace PikchaWebApp.Test.Unit
{
    public class StorageManagerTest
    {
        [Fact]
        public async Task UploadToLocalDirectory()
        {
            var mockRepoWebHost = new Mock<IWebHostEnvironment>();
            mockRepoWebHost.Setup(x => x.WebRootPath)
                  .Returns("./");

            var mockRepoConfig = new Mock<IConfiguration>();
            var configurationSection = new Mock<IConfigurationSection>();
            configurationSection.Setup(a => a.Value).Returns("/uploads/avatars");
            mockRepoConfig.Setup(a => a.GetSection("UploadDirectories.Avatar")).Returns(configurationSection.Object);

            StorageManager manager = new StorageManager(mockRepoWebHost.Object, mockRepoConfig.Object);

            Mock<IFormFile> mockFile = MockHelpers.CreateNewFile("This is test file", "test.txt");

            string filePath = string.Empty;
            await manager.UploadToLocalDirectory(mockFile.Object, StorageManager.FileCategory.Avatar, ref filePath);
            
            Assert.Contains("uploads/avatars", filePath);
        }

    }
}

// https://dzone.com/articles/file-uploads-in-aspnet-core-integration-tests
