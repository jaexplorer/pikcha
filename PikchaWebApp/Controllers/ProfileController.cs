using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PikchaWebApp.Data;
using PikchaWebApp.Managers;
using PikchaWebApp.Models;

namespace PikchaWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProfileController : ControllerBase
    {
        protected readonly IWebHostEnvironment _hostingEnvironment;
        protected readonly IConfiguration _configuration;
        protected readonly UserManager<PikchaUser> _userManager;
        protected readonly PikchaDbContext _pikchDbContext;

        public ProfileController(IWebHostEnvironment hostingEnvironment, IConfiguration configuration, UserManager<PikchaUser> userManager, PikchaDbContext pikchDbContext)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
            _userManager = userManager;
            _pikchDbContext = pikchDbContext;
        }

        // GET: api/profile/list
        [HttpGet("/list")]
        public async Task<ReturnDataModel> List()
        {
            //var users = await _userManager.Users.ToListAsync();
            var count = _userManager.Users.Count();
            var users = await Task.FromResult(_userManager.Users.ToList());
            return new ReturnDataModel() { Data = users };
        }

        // GET: api/profile/5
        [HttpGet("{id}")]
        public async Task<ReturnDataModel> Get(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return new ReturnDataModel() { Data = user };
        }
               

        // PUT: api/profile/5
        [HttpPut("{id}")]
        public async Task<ReturnDataModel> Put(int id, [FromBody] ProfileViewModel profModel)
        {
            PikchaUser pikchaUser = _userManager.FindByIdAsync(profModel.Id).Result;
            pikchaUser.CopyPropertiesFrom(profModel);

            IdentityResult result = await _userManager.UpdateAsync(pikchaUser);
            if (result.Succeeded)
            {
                return new ReturnDataModel() { Data = profModel };
            }
            return new ReturnDataModel() { Status = "Error", Statuscode = "401" };
        }

        // DELETE: api/profile/5
        [HttpDelete("{id}")]
        public async void Delete(string userId)
        {
            PikchaUser pikchaUser = _userManager.FindByIdAsync(userId).Result;

            await _userManager.DeleteAsync(pikchaUser);

        }

        // POST: api/profile/avatar
        [HttpPost("/avatar")]
        public async Task<ReturnDataModel> UploadAvatarImage(IFormFile avatarFile)
        {

            StorageManager manager = new StorageManager(_hostingEnvironment, _configuration);
            // = "";
            Task<string> copyTask = manager.UploadToLocalDirectory(avatarFile, StorageManager.FileCategory.Avatar);
            string filePath = copyTask.Result;
            // get the PikchaUser from ClaimsPrincipal {{this.User}} and save the file location
            Task<PikchaUser> loggedinUserTask = _userManager.GetUserAsync(this.User);

            await Task.WhenAll(copyTask, loggedinUserTask);

            PikchaUser loggedinUser = loggedinUserTask.Result;
            if (copyTask.IsCompleted)
            {
                loggedinUser.AvatarFileName = filePath;
                await _pikchDbContext.SaveChangesAsync();
            }
            return new ReturnDataModel() { Data = filePath };
        }

        // POST: api/profile/signature
        [HttpPost("/signature")]
        public async Task<ReturnDataModel> UploadSignatureImage(IFormFile signatureFile)
        {

            StorageManager manager = new StorageManager(_hostingEnvironment, _configuration);
            Task<string> copyTask = manager.UploadToLocalDirectory(signatureFile, StorageManager.FileCategory.Signature);
            string filePath = copyTask.Result;

            // get the PikchaUser from ClaimsPrincipal {{this.User}} and save the file location
            Task<PikchaUser> loggedinUserTask = _userManager.GetUserAsync(this.User);

            await Task.WhenAll(copyTask, loggedinUserTask);

            PikchaUser loggedinUser = loggedinUserTask.Result;
            if (copyTask.IsCompleted)
            {
                loggedinUser.SignatureFileName = filePath;
                await _pikchDbContext.SaveChangesAsync();
            }

            return new ReturnDataModel() { Data = filePath };
        }
    }

    public class ProfileViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Address_1 { get; set; }
        public string Address_2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}