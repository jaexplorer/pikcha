﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        /*[HttpGet("list")]
        public async Task<ActionResult> List()
        {
            //var users = await _userManager.Users.ToListAsync();
            var users = await Task.FromResult(_userManager.Users.ToList());
            return ReturnOkOrErrorStatus(users);
            //return new ReturnDataModel() { Data = users };
        } */

        // GET: api/profile/5
        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetUser(string userId)
        {
            //var user = await _userManager.FindByIdAsync(userId);
            // TO DO :  if the request user is admin, return profile based on userId query
            //var pikchaUser = await _userManager.GetUserAsync(this.User);
            var pikchaUser = await _pikchDbContext.PikchaUsers.FindAsync(userId);
            
            var userDTO = _mapper.Map<PikchaArtistDTO>(pikchaUser);

            var roles = await _userManager.GetRolesAsync(pikchaUser);
            if(userDTO != null && roles != null)
            {
                userDTO.Roles = roles.ToList();
            }

            return ReturnOkOrErrorStatus(userDTO);
        }


        // PUT: api/profile/5
        [HttpPut("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<ActionResult> UpdateUser(string userId, [FromBody] ProfileViewModel userInfo)
        {
            try
            {
                var pikchaUser = await _userManager.GetUserAsync(this.User); ;

                if(pikchaUser == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "User not found.");
                }
                //await loggedinUserTask;
                pikchaUser.CopyPropertiesFrom(userInfo);
                //pikchaUser.Addr2 = userInfo.

                IdentityResult result = await _userManager.UpdateAsync(pikchaUser);
                if (result.Succeeded)
                {
                    var lgUser = _mapper.Map<PikchaAuthenticatedUserDTO>(pikchaUser);
                    var roles = await _userManager.GetRolesAsync(pikchaUser);
                    if (roles != null)
                    {
                        lgUser.Roles = roles.ToList();
                    }

                    return Ok(lgUser);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in updating the user information.");
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
        [HttpPost("avatar/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<ActionResult> UploadAvatarImage(string userId, [FromBody] IFormFile imageFile)
        {

            try
            {
                StorageManager manager = new StorageManager(_hostingEnvironment, _configuration);
                // = "";
                Task<string> copyTask = manager.UploadToLocalDirectory(imageFile, Guid.NewGuid().ToString(), ".jpg", StorageManager.FileCategory.Avatar);
                string filePath = copyTask.Result;
                // get the PikchaUser from ClaimsPrincipal {{this.User}} and save the file location
                Task<PikchaUser> loggedinUserTask = _userManager.GetUserAsync(this.User);

                await Task.WhenAll(copyTask, loggedinUserTask);

                PikchaUser pikchaUser = loggedinUserTask.Result;

                if(pikchaUser == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "User not found.");

                }

                if (copyTask.IsCompleted)
                {
                    pikchaUser.Avatar = filePath;
                    await _pikchDbContext.SaveChangesAsync();
                    var lgUSer = _mapper.Map<PikchaAuthenticatedUserDTO>(pikchaUser);
                    var roles = await _userManager.GetRolesAsync(pikchaUser);

                    if (roles != null)
                    {
                        lgUSer.Roles = roles.ToList();
                    }
                    return Ok(lgUSer);

                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in uploading the profile image.");


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }

        }

        // TO DO : This api is not required. 
        // POST: api/profile/signature
        //[HttpPost("signature")]
        //public async Task<ReturnDataModel> UploadSignatureImage([FromForm] IFormFile signatureFile)
        //{

        //    StorageManager manager = new StorageManager(_hostingEnvironment, _configuration);
        //    Task<string> copyTask = manager.UploadToLocalDirectory(signatureFile, Guid.NewGuid().ToString(), ".jpg", StorageManager.FileCategory.Sign);
        //    string filePath = copyTask.Result;

        //    // get the PikchaUser from ClaimsPrincipal {{this.User}} and save the file location
        //    Task<PikchaUser> loggedinUserTask = _userManager.GetUserAsync(this.User);

        //    await Task.WhenAll(copyTask, loggedinUserTask);

        //    PikchaUser loggedinUser = loggedinUserTask.Result;
        //    if (copyTask.IsCompleted)
        //    {
        //        loggedinUser.SignatureFileName = filePath;
        //        await _pikchDbContext.SaveChangesAsync();
        //    }

        //    return new ReturnDataModel() { Data = filePath };
        //}

        [Authorize]
        [HttpPost("artist/promote/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PromoteUserToPhotographer(string userId, [FromBody] IFormFile signatureFile)
        {
            try
            {
                var pikchaUser = await _userManager.GetUserAsync(this.User);
                if (pikchaUser == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, PikchaMessages.MESS_Status404_UserNotFound);
                }
                bool isInAlready = await _userManager.IsInRoleAsync(pikchaUser, PikchaConstants.PIKCHA_ROLES_PHOTOGRAPHER_NAME);
                if(isInAlready)
                {
                    return StatusCode(StatusCodes.Status404NotFound, PikchaMessages.MESS_Status404_UserAlreadyPromoted);
                }

                // upload signature file
                ImageProcessingManager manager = new ImageProcessingManager(_hostingEnvironment, _configuration);
                //string filePath = await manager.UploadToLocalDirectory(signatureFile, Guid.NewGuid().ToString(), ".jpg", StorageManager.FileCategory.Sign);
                string orgFName = string.Empty;
                string invFName = string.Empty;

                manager.ProcessSignatureFile(signatureFile, ref orgFName, ref invFName);

                pikchaUser.Sign = orgFName;
                pikchaUser.InvSign = invFName;
                await _pikchDbContext.SaveChangesAsync();
              
                var result = await _userManager.AddToRoleAsync(pikchaUser, PikchaConstants.PIKCHA_ROLES_PHOTOGRAPHER_NAME);

                if(result.Succeeded)
                {
                    var pkUsr = _mapper.Map<PikchaAuthenticatedUserDTO>(pikchaUser);
                    var roles = await _userManager.GetRolesAsync(pikchaUser);
                    if (roles != null)
                    {
                        pkUsr.Roles = roles.ToList();
                    }
                    return Ok(pkUsr);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in promoting the user ");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }


        [Authorize]
        [HttpGet("artist/signature/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetSignature(string userId)
        {
            var pikchaUser = await _userManager.GetUserAsync(this.User);
            if (pikchaUser == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, PikchaMessages.MESS_Status404_UserNotFound);
            }
            return Ok(new { OrgSig = pikchaUser.Sign ?? string.Empty, InvSig = pikchaUser.InvSign ?? string.Empty});
        }

        [Authorize]
        [HttpGet("myinfo/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> LoggedInUserInfo( string userId)
        {
            var pikchaUser = await _userManager.GetUserAsync(this.User);

            var userDTO = _mapper.Map<PikchaAuthenticatedUserDTO>(pikchaUser);
            var roles = await _userManager.GetRolesAsync(pikchaUser);

            if (roles != null)
            {
                userDTO.Roles = roles.ToList();
            }
            return ReturnOkOrErrorStatus(userDTO);

        }

        [Authorize]
        [HttpPost("artist/follow/{artistId}/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> FollowAnArtist(string artistId, string userId)
        {
            try
            {
                // var tmpp = _userManager.GetUserAsync(this.User).Result;
                // var tmpp2 = _userManager.GetUserAsync(HttpContext.User).Result;
                var pikchaUser = await _userManager.GetUserAsync(this.User);
                if(_pikchDbContext.PikchaUsers.Find(artistId) == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, PikchaMessages.MESS_Status404NotFound);
                }
                pikchaUser.Following.Add(new PikchaArtistFollower() { ArtistsId = artistId });
                await _pikchDbContext.SaveChangesAsync();

                var qUser = _pikchDbContext.PikchaUsers.Include("Following.PikchaArtist").First(u => u.Id == pikchaUser.Id);
                var lgUSer = _mapper.Map<PikchaAuthenticatedUserDTO>(qUser);
                var roles = await _userManager.GetRolesAsync(pikchaUser);

                if (roles != null)
                {
                    lgUSer.Roles = roles.ToList();
                }
                //lgUSer.Following = _mapper.ProjectTo<PikchaAuthenticatedUserDTO>(qUser.Following.ToList());
                return ReturnOkOrErrorStatus(lgUSer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [Authorize]
        [HttpPost("artist/unfollow/{artistId}/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UnFollowAnArtist(string artistId, string userId)
        {
            try
            {
                var pikchaUser = await _userManager.GetUserAsync(this.User);

                var folArt = pikchaUser.Following.First(f => f.ArtistsId == artistId && f.UserId == userId);

                _pikchDbContext.Remove(folArt);
                await _pikchDbContext.SaveChangesAsync();

                var qUser = _pikchDbContext.PikchaUsers.Include("Following.PikchaArtist").First(u => u.Id == pikchaUser.Id);
                var lgUSer = _mapper.Map<PikchaAuthenticatedUserDTO>(qUser);
                var roles = await _userManager.GetRolesAsync(pikchaUser);

                if (roles != null)
                {
                    lgUSer.Roles = roles.ToList();
                }
                //lgUSer.Following = _mapper.ProjectTo<PikchaAuthenticatedUserDTO>(qUser.Following.ToList());
                return ReturnOkOrErrorStatus(lgUSer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }

    public class ProfileViewModel
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string Bio { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string City { get; set; }
        public string Postal { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public Dictionary<string, string> Links { get; set; }

    }




}