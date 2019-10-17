using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using PikchaWebApp.Test.Shared;
using PikchaWebApp.Data;
using PikchaWebApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using PikchaWebApp.Controllers;
using System.Threading;
using System.Security.Claims;
using PikchaWebApp.Managers;

namespace PikchaWebApp.Test
{
    public static class MockHelpers
    {
        public static StringBuilder LogMessage = new StringBuilder();

        /*
        public static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());
            return mgr;
        }

        public static Mock<RoleManager<TRole>> MockRoleManager<TRole>(IRoleStore<TRole> store = null) where TRole : class
        {
            store = store ?? new Mock<IRoleStore<TRole>>().Object;
            var roles = new List<IRoleValidator<TRole>>();
            roles.Add(new RoleValidator<TRole>());
            return new Mock<RoleManager<TRole>>(store, roles, new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(), null);
        }
        */
        public static Mock<ILogger<T>> MockILogger<T>(StringBuilder logStore = null) where T : class
        {
            logStore = logStore ?? LogMessage;
            var logger = new Mock<ILogger<T>>();
            logger.Setup(x => x.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<object>(),
                It.IsAny<Exception>(), It.IsAny<Func<object, Exception, string>>()))
                .Callback((LogLevel logLevel, EventId eventId, object state, Exception exception, Func<object, Exception, string> formatter) =>
                {
                    if (formatter == null)
                    {
                        logStore.Append(state.ToString());
                    }
                    else
                    {
                        logStore.Append(formatter(state, exception));
                    }
                    logStore.Append(" ");
                });
            logger.Setup(x => x.BeginScope(It.IsAny<object>())).Callback((object state) =>
            {
                logStore.Append(state.ToString());
                logStore.Append(" ");
            });
            logger.Setup(x => x.IsEnabled(LogLevel.Debug)).Returns(true);
            logger.Setup(x => x.IsEnabled(LogLevel.Warning)).Returns(true);

            return logger;
        }

        public static UserManager<TUser> TestUserManager<TUser>(IUserStore<TUser> store = null) where TUser : class
        {
            store = store ?? new Mock<IUserStore<TUser>>().Object;
            var options = new Mock<IOptions<IdentityOptions>>();
            var idOptions = new IdentityOptions();
            idOptions.Lockout.AllowedForNewUsers = false;
            options.Setup(o => o.Value).Returns(idOptions);
            var userValidators = new List<IUserValidator<TUser>>();
            var validator = new Mock<IUserValidator<TUser>>();
            userValidators.Add(validator.Object);
            var pwdValidators = new List<PasswordValidator<TUser>>();
            pwdValidators.Add(new PasswordValidator<TUser>());
            var userManager = new UserManager<TUser>(store, options.Object, new PasswordHasher<TUser>(),
                userValidators, pwdValidators, new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(), null,
                new Mock<ILogger<UserManager<TUser>>>().Object);
            validator.Setup(v => v.ValidateAsync(userManager, It.IsAny<TUser>()))
                .Returns(Task.FromResult(IdentityResult.Success)).Verifiable();
            return userManager;
        }

        public static RoleManager<TRole> TestRoleManager<TRole>(IRoleStore<TRole> store = null) where TRole : class
        {
            store = store ?? new Mock<IRoleStore<TRole>>().Object;
            var roles = new List<IRoleValidator<TRole>>();
            roles.Add(new RoleValidator<TRole>());
            return new AspNetRoleManager<TRole>(store, roles,
                new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(),
                null,
                null);
        }

        public static IFormFile CreateNewImageFile(string fileUri, string fileName, string name)
        {
            //Arrange
            // var fileMock = new Mock<IFormFile>();
            //Setup mock file using a memory stream
            //var content = "Hello World from a Fake File";
            //var fileName = "test.pdf";
            try
            {
                var imageFile = File.OpenRead(fileUri);

                IFormFile file = new FormFile(imageFile, 0, imageFile.Length, name, fileUri);
                return file;

            }
            catch (Exception ex)
            {
                var cc = ex;
                return null;
            }


            //    var ms = new MemoryStream();
            //var writer = new StreamWriter(ms);
            //writer.Write(content);
            //writer.Flush();
            //ms.Position = 0;
            //fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            //fileMock.Setup(_ => _.FileName).Returns(fileName);
            //fileMock.Setup(_ => _.Length).Returns(ms.Length);

            //return fileMock;
        }


        public static PikchaDbContext MockPikchaDBContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<PikchaDbContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "Add_writes_to_database");
            var _dbContext = new PikchaDbContext(optionsBuilder.Options, null);

            return _dbContext;
            // https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/in-memory
        }

        public static IEnumerable<PikchaUser> CreateUsersinDB(int numberOfUsers, DbContext dbContext)
        {
            //PasswordHasher<PikchaUser> hasher = new Microsoft.AspNetCore.Identity.PasswordHasher<PikchaUser>();

            var users = Enumerable.Range(1, numberOfUsers).Select(n =>
                 new PikchaUser
                 {
                     UserName = "test_user" + n + "@test.thananji.com",
                     Email = "test_user" + n + "@test.thananji.com"

                 });

            dbContext.AddRange(users);
            dbContext.SaveChanges();
            return users;
        }

        public static async Task<PikchaUser> CreateNewUser(int pikchaUserId, string userId, string userName, string password, SqliteInMemoryFixture fixture)
        {
            // Arrange
            var userManager = fixture.ServiceProvider.GetRequiredService<UserManager<PikchaUser>>();
           
            var user = new PikchaUser {PikchaUserId = pikchaUserId, Id = userId, UserName = userName, Email = userName, TwoFactorEnabled=false };
            
            await userManager.CreateAsync(user, password);
            
            return user;

        }


        public static UserManager<PikchaUser> CreateMockUserManager(PikchaUser pkUser)
        {
            Mock<IUserRoleStore<PikchaUser>> MockUserStore = new Mock<IUserStore<PikchaUser>>().As<IUserRoleStore<PikchaUser>>();
            
            MockUserStore.Setup(x => x.FindByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(
            (string userName, CancellationToken token) => pkUser);


            MockUserStore.Setup(x => x.UpdateAsync(It.IsAny<PikchaUser>(), It.IsAny<CancellationToken>())).ReturnsAsync(
            (PikchaUser userName, CancellationToken token) => IdentityResult.Success);

            
            MockUserStore.Setup(x => x.AddToRoleAsync(It.IsAny<PikchaUser>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(
            Task.FromResult(IdentityResult.Success));

            List<string> roles = new List<string>();
            roles.Add(PikchaConstants.PIKCHA_ROLES_PHOTOGRAPHER_NAME);
            MockUserStore.Setup(x => x.GetRolesAsync(It.IsAny<PikchaUser>(), It.IsAny<CancellationToken>())).ReturnsAsync(
            (PikchaUser userName, CancellationToken token) => roles);

            return new UserManager<PikchaUser>(MockUserStore.Object, null, null, null, null, null, null, null, null);

        }
        public static async Task<PikchaUser> FindUserById(string userId, SqliteInMemoryFixture fixture)
        {
            var userManager = fixture.ServiceProvider.GetRequiredService<UserManager<PikchaUser>>();
            var task = await userManager.FindByIdAsync(userId);
            return task;
        }

        public static async Task DeleteUser(PikchaUser user, SqliteInMemoryFixture fixture)
        {
            var userManager = fixture.ServiceProvider.GetRequiredService<UserManager<PikchaUser>>();

            await userManager.DeleteAsync(user);

        }

        public static void MockRandomImages(ref Mock<IMapper> _imapper)
        {
            _imapper.Setup(x => x.Map<PikchaRandomImageDTO>(It.IsAny<PikchaImage>()))
      .Returns((PikchaImage source) => new PikchaRandomImageDTO() { });
           

        }

        public static ImageViewModel CreateImage(string title, string caption, string location)
        {
            IFormFile imgFile = MockHelpers.CreateNewImageFile("TestPhotos/beach-jaffna.jpg", "beach-jaffna.jpg", "beach-jaffna");

            ImageViewModel vmImage = new ImageViewModel();
            vmImage.Title = title;
            vmImage.Caption = caption;
            vmImage.Location = location;
            //vmImage.NumberOfPrint = 10;
            vmImage.ImageFile = imgFile;

            return vmImage;
        }

    }
}
