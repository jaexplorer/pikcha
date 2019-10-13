using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

    public class ProfileController : PikchaBaseController
    {
        protected readonly IWebHostEnvironment _hostingEnvironment;
        protected readonly IConfiguration _configuration;
        protected readonly UserManager<PikchaUser> _userManager;
        protected readonly PikchaDbContext _pikchDbContext;
        private readonly IMapper _mapper;

        public ProfileController(IWebHostEnvironment hostingEnvironment, IConfiguration configuration, UserManager<PikchaUser> userManager, PikchaDbContext pikchDbContext, IMapper mapper)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
            _userManager = userManager;
            _pikchDbContext = pikchDbContext;
            _mapper = mapper;
        }

        // GET: api/profile/list
        [HttpGet("list")]
        public async Task<ActionResult> List()
        {
            //var users = await _userManager.Users.ToListAsync();
            var users = await Task.FromResult(_userManager.Users.ToList());
            return ReturnOkOrNotFound(users);
            //return new ReturnDataModel() { Data = users };
        }

        // GET: api/profile/5
        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Get(string userId)
        {
            //var user = await _userManager.FindByIdAsync(userId);
            // TO DO :  if the reu=quest user is admin, return profile based on userId query
            var pikchaUser = await _userManager.GetUserAsync(this.User); ;
            return ReturnOkOrNotFound(pikchaUser);
            //return Ok(pikchaUser);
            //return new ReturnDataModel() { Data = user };
        }


        // PUT: api/profile/5
        [HttpPut("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<ActionResult> Put(int id, [FromForm] ProfileViewModel profModel)
        {
            try
            {
                //PikchaUser pikchaUser = _userManager.FindByIdAsync(profModel.Id).Result;
                // Task<PikchaUser> loggedinUserTask = _userManager.GetUserAsync(this.User);
                var pikchaUser = await _userManager.GetUserAsync(this.User); ;
                //await loggedinUserTask;
                pikchaUser.CopyPropertiesFrom(profModel);

                IdentityResult result = await _userManager.UpdateAsync(pikchaUser);
                if (result.Succeeded)
                {
                    return ReturnOkOrNotFound(pikchaUser);
                    // return Ok(pikchaUser);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Details are not updated.");

                /* if (loggedinUserTask.IsCompleted)
                {
                    //PikchaUser pikchaUser = loggedinUserTask.Result;
                   

                }

                return StatusCode(StatusCodes.Status500InternalServerError, "Task is not completed."); */

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        // DELETE: api/profile/5
        [HttpDelete("{userId}")]
        public async void Delete(string userId)
        {
            PikchaUser pikchaUser = _userManager.FindByIdAsync(userId).Result;

            await _userManager.DeleteAsync(pikchaUser);

        }

        // POST: api/profile/avatar
        [HttpPost("avatar")]
        public async Task<ActionResult> UploadAvatarImage([FromForm] IFormFile avatarFile)
        {

            try
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
                    return Ok(filePath);

                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Task is not completed.");


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }

        }

        // POST: api/profile/signature
        [HttpPost("signature")]
        public async Task<ReturnDataModel> UploadSignatureImage([FromForm] IFormFile signatureFile)
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

        [Authorize]
        [HttpPost("promotetophotographer/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PromoteUserToPhotographer([FromForm] string userId)
        {
            try
            {
                var pikchaUser = await _userManager.GetUserAsync(this.User);
                var result = await _userManager.AddToRoleAsync(pikchaUser, PikchaConstants.PIKCHA_ROLES_PHOTOGRAPHER_NAME);

                if(result.Succeeded)
                {
                    return Ok();
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in promoting the user ");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        [Authorize]
        [HttpGet("loggedinuserinfo/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> LoggedInUserInfo( string userId)
        {
            try
            {
                var pikchaUser = await _userManager.GetUserAsync(this.User);

                var lgUSer =  _mapper.Map<PikchaLoggedInUserDTO>(_pikchDbContext.PikchaUsers.Find(pikchaUser.Id));
                lgUSer.IsPhotoGrapher = await _userManager.IsInRoleAsync(pikchaUser, PikchaConstants.PIKCHA_ROLES_PHOTOGRAPHER_NAME);
                return ReturnOkOrNotFound(lgUSer);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }


        }


    }

    public class ProfileViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BioInfo { get; set; }
        public string PerAddress1 { get; set; }
        public string PerAddress2 { get; set; }
        public string PerCity { get; set; }
        public string PerPostalCode { get; set; }
        public string PerCountry { get; set; }
        public string ShipAddress1 { get; set; }
        public string ShipAddress2 { get; set; }
        public string ShipCity { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
        public string FacebookLink { get; set; }
        public string InstagramLink { get; set; }
        public string LinkedInLink { get; set; }
    }
}