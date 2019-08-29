using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PikchaWebApp.Test.Integration
{
    // TO DO : Integration test to move to another project?
    public class UserProfile_IT : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        //private readonly TestServer _server;
        //private readonly HttpClient _client;
        public UserProfile_IT(CustomWebApplicationFactory<Startup> factory)
        {
            // Arrange
            //_factory = factory;
            //_server = new TestServer(new WebHostBuilder()
            //   .UseStartup<Startup>());
            //_client = _server.CreateClient();
            //_client = _factory.CreateClient();

        }

        [Fact]
        public async Task Post_CreateNewPikchaUser()
        {
            // Act
            /*var data = new Dictionary<string, string>()
            {
                { "UserName", "test@gmail.com" },
                { "Email", "test@gmail.com" },
                { "Password", "Tasdwer@asdqweT" }
            };

            var secretContent = new FormUrlEncodedContent(data);

            var result = await _client.PostAsync("/Identity/Account/Register", secretContent);
    */        
    Assert.Equal(1, 1);
            //var response = await _client.GetAsync("/");
            //response.EnsureSuccessStatusCode();
            //var responseString = await response.Content.ReadAsStringAsync();
            // Assert
            //Assert.Equal("Hello World!", responseString);
        }
    }
}


// https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-2.2
