using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PikchaWebApp.Data;
using PikchaWebApp.Managers;
using PikchaWebApp.Models;

namespace PikchaWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        protected readonly IWebHostEnvironment _hostingEnvironment;
        protected readonly IConfiguration _configuration;
        protected readonly UserManager<PikchaUser> _userManager;
        protected readonly PikchaDbContext _pikchDbContext;

        public UserProfileController(IWebHostEnvironment hostingEnvironment, IConfiguration configuration, UserManager<PikchaUser> userManager, PikchaDbContext pikchDbContext)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
            _userManager = userManager;
            _pikchDbContext = pikchDbContext;
        }

        // GET: api/UserProfile
        [HttpGet]
        public async Task<ReturnDataModel> Get()
        {
            var users = _userManager.Users; 
            return new ReturnDataModel() { Data = await users.ToListAsync() };
        }

        // GET: api/UserProfile/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/UserProfile
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/UserProfile/5
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
            return new ReturnDataModel() { Status="Error", Statuscode="401" };                       
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // POST: api/UserProfile/avatar
        [HttpPost("avatar")]
        public async Task<ReturnDataModel> UploadAvatarImage(IFormFile avatarFile)
        {

            StorageManager manager = new StorageManager(_hostingEnvironment, _configuration);
            string filePath = "";
            Task copyTask = manager.UploadToLocalDirectory(avatarFile, StorageManager.FileCategory.Avatar, ref filePath);
                        
            // get the PikchaUser from ClaimsPrincipal {{this.User}} and save the file location
            Task<PikchaUser> loggedinUserTask = _userManager.GetUserAsync(this.User);

            await Task.WhenAll(copyTask, loggedinUserTask);

            PikchaUser loggedinUser = loggedinUserTask.Result;
            if(copyTask.IsCompleted)
            {
                loggedinUser.AvatarFileName = filePath;
                await _pikchDbContext.SaveChangesAsync();
            }

            return new ReturnDataModel();
        }

        // POST: api/UserProfile/signature
        [HttpPost("signature")]
        public async Task<ReturnDataModel> UploadSignatureImage(IFormFile signatureFile)
        {

            StorageManager manager = new StorageManager(_hostingEnvironment, _configuration);
            string filePath = "";
            Task copyTask = manager.UploadToLocalDirectory(signatureFile, StorageManager.FileCategory.Signature, ref filePath);

            // get the PikchaUser from ClaimsPrincipal {{this.User}} and save the file location
            Task<PikchaUser> loggedinUserTask = _userManager.GetUserAsync(this.User);

            await Task.WhenAll(copyTask, loggedinUserTask);

            PikchaUser loggedinUser = loggedinUserTask.Result;
            if (copyTask.IsCompleted)
            {
                loggedinUser.SignatureFileName = filePath;
                await _pikchDbContext.SaveChangesAsync();
            }
            
            return new ReturnDataModel();
        }
    }

    public class ProfileViewModel
    {
        public string Id;
        public string FirstName;
        public string LastName;

        public string Address_1;
        public string Address_2;
        public string City;
        public string PostalCode;
        public string Country;
    }
}
