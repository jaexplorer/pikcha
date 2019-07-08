using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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

        public UserProfileController(IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: api/UserProfile
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // POST: api/UserProfile/avatar
        [HttpPost("avatar")]
        public void UploadAvatarImage(IFormFile avatarFile)
        {

            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, _configuration["UploadDirectories.Avatar"]);
            var filePath = Path.Combine(uploads, avatarFile.FileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                avatarFile.CopyToAsync(fileStream);
            }
            // get the PikchaUser from ClaimsPrincipal {{this.User}} and save the file location
            PikchaUser loggedinUser = _userManager.GetUserAsync(this.User).Result;
        }
    }

    public class ProfileViewModel
    {
        public string FirstName;
        public string LastName;

        public string Address_1;
        public string Address_2;
        public string City;
        public string PostalCode;
        public string Country;

    }
}
