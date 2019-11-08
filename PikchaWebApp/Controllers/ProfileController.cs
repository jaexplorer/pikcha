using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PikchaWebApp.Data;
using PikchaWebApp.Managers;
using PikchaWebApp.Models;
using Serilog;

namespace PikchaWebApp.Controllers
{
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

        // GET: api/profile/artist/5
        [HttpGet("artist/{artistId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetArtist(string artistId)
        {
            // TO DO :  if the request user is admin, return profile based on artistId query

            try
            {
                var pikchaUser = await _pikchDbContext.PikchaUsers.Include("Images.Views").Include("Following.Artist").Include("Followers.PikchaUser").FirstAsync(u => u.Id == artistId);
                var userDTO = _mapper.Map<ArtistDTO>(pikchaUser);
                
               // var pikchaUserQuery =  _pikchDbContext.PikchaUsers.Include("Images.Views").Include("Following").Include("Following.PikchaUser").Include("Following.Artist").Where(u => u.Id == artistId);
                //var userDTO = await _mapper.ProjectTo<ArtistDTO>(pikchaUserQuery).FirstAsync();
                //var userDTO = await _mapper.ProjectTo<ArtistDTO>(pikchaUserQuery).FirstAsync();

                return ReturnOkOrErrorStatus(userDTO);
            }
            catch(Exception ex)
            {
                Log.Error(ex, " Profile, GetArtist, artistId ={artistId} ", artistId);
                return StatusCode(StatusCodes.Status404NotFound, PikchaMessages.MESS_Status404_UserNotFound);
            }
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
                pikchaUser.CopyPropertiesFrom(userInfo);

                IdentityResult result = await _userManager.UpdateAsync(pikchaUser);
                if (result.Succeeded)
                {
                    var lgUser = _mapper.Map<AuthenticatedUserDTO>(pikchaUser);
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
                Log.Error(ex, " Profile, GetArtist, userId ={userId} ", userId);
                return StatusCode(StatusCodes.Status500InternalServerError, PikchaMessages.MESS_Status500InternalServerError);
            }

        }

        [HttpPut("artist/{artistId}/links")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<ActionResult> UpdateLinks(string userId, [FromBody] LinkVM sLink)
        {
            try
            {
                var pikchaUser = await _userManager.GetUserAsync(this.User); ;
                if (pikchaUser == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, PikchaMessages.MESS_Status404_UserNotFound);
                }
                pikchaUser.Links[sLink.Type] = sLink.Url;                

                IdentityResult result = await _userManager.UpdateAsync(pikchaUser);
                if (result.Succeeded)
                {
                    //var pikchaUserQuery = _pikchDbContext.PikchaUsers.Include("Images.Views").Include("Following").Include("Following.PikchaUser").Include("Following.Artist").Where(u => u.Id == pikchaUser.Id);
                    //var userDTO = await _mapper.ProjectTo<ArtistDTO>(pikchaUserQuery).FirstAsync();
                    var pikchaUserDb = await _pikchDbContext.PikchaUsers.Include("Images.Views").Include("Following.Artist").Include("Followers.PikchaUser").FirstAsync(u => u.Id ==pikchaUser.Id);
                    var userDTO = _mapper.Map<ArtistDTO>(pikchaUserDb);
                    return ReturnOkOrErrorStatus(userDTO);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in updating the user information.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, " Profile, GetArtist, userId ={userId} ", userId);
                return StatusCode(StatusCodes.Status500InternalServerError, PikchaMessages.MESS_Status500InternalServerError);
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
        [HttpPost("{userId}/avatar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<ActionResult> UploadAvatarImage(string userId)
        {
            try
            {
                using (var streamReader = new HttpRequestStreamReader(Request.Body, Encoding.UTF8))
                using (var jsonReader = new JsonTextReader(streamReader))
                {
                    var json = await JObject.LoadAsync(jsonReader);

                    string avatarContent = (string) json["avatarContent"];
                    if (avatarContent.Contains(','))
                    {
                        avatarContent = avatarContent.Split(",")[1];
                    }

                    //string tmp = (string)json["signatureContent"]["data"];
                    ImageProcessingManager manager = new ImageProcessingManager(_hostingEnvironment, _configuration);
                    // = "";
                    string filePath = string.Empty;
                    manager.ProcessAvatarFile(avatarContent, ref filePath);
                    // get the PikchaUser from ClaimsPrincipal {{this.User}} and save the file location
                    PikchaUser pikchaUser = await _userManager.GetUserAsync(this.User);

                    if (pikchaUser == null)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, PikchaMessages.MESS_Status404_UserNotFound);

                    }

                    if (!string.IsNullOrEmpty(filePath))
                    {
                        pikchaUser.Avatar = filePath;
                        await _pikchDbContext.SaveChangesAsync();
                        var lgUSer = _mapper.Map<AuthenticatedUserDTO>(pikchaUser);
                        var roles = await _userManager.GetRolesAsync(pikchaUser);

                        if (roles != null)
                        {
                            lgUSer.Roles = roles.ToList();
                        }
                        return Ok(lgUSer);

                    }
                    else
                    {
                        Log.Error(" Profile, UploadAvatarImage, userId ={userId}, filePath={filePath}", userId, filePath);
                        return StatusCode(StatusCodes.Status500InternalServerError, PikchaMessages.MESS_Status500InternalServerError);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, " Profile, UploadAvatarImage, userId ={userId}", userId);
                return StatusCode(StatusCodes.Status500InternalServerError, PikchaMessages.MESS_Status404ImageNotFound);

            }
        }


        [HttpPost("artist/{artistId}/cover")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<ActionResult> UploadCoverImage(string artistId)
        {
            try
            {
                PikchaUser pikchaUser = await _userManager.GetUserAsync(this.User);
                if (pikchaUser == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, PikchaMessages.MESS_Status404_UserNotFound);
                }

               /* bool isArtist = await _userManager.IsInRoleAsync(pikchaUser, PikchaConstants.PIKCHA_ROLES_ARTIST_NAME);
                if(! isArtist)
                {
                    return StatusCode(StatusCodes.Status404NotFound, PikchaMessages.MESS_Status404_ArtistNotFound);
                } */
                using (var streamReader = new HttpRequestStreamReader(Request.Body, Encoding.UTF8))
                using (var jsonReader = new JsonTextReader(streamReader))
                {
                    var json = await JObject.LoadAsync(jsonReader);

                    string coverContent = (string)json["coverContent"];
                    if (coverContent.Contains(','))
                    {
                        coverContent = coverContent.Split(",")[1];
                    }

                    //string tmp = (string)json["signatureContent"]["data"];
                    ImageProcessingManager manager = new ImageProcessingManager(_hostingEnvironment, _configuration);
                    // = "";
                    string filePath = string.Empty;
                    manager.ProcessCoverImage(coverContent, ref filePath);
                    // get the PikchaUser from ClaimsPrincipal {{this.User}} and save the file location

                    if (!string.IsNullOrEmpty(filePath))
                    {
                        pikchaUser.Cover = filePath;
                        await _pikchDbContext.SaveChangesAsync();

                        //var pikchaUserQuery = _pikchDbContext.PikchaUsers.Include("Images.Views").Include("Following").Include("Following.PikchaUser").Include("Following.Artist").Where(u => u.Id == artistId);
                        // var userDTO = await _mapper.ProjectTo<ArtistDTO>(pikchaUserQuery).FirstAsync();
                        var pikchaUserDb = await _pikchDbContext.PikchaUsers.Include("Images.Views").Include("Following.Artist").Include("Followers.PikchaUser").FirstAsync(u => u.Id == pikchaUser.Id);
                        var userDTO = _mapper.Map<ArtistDTO>(pikchaUserDb);
                        return ReturnOkOrErrorStatus(userDTO);

                    }
                    else
                    {
                        Log.Error(" Profile, UploadCoverImage, userId ={userId}, filePath={filePath}", artistId, filePath);
                        return StatusCode(StatusCodes.Status500InternalServerError, PikchaMessages.MESS_Status500InternalServerError);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, " Profile, UploadCoverImage, userId ={userId}", artistId);
                return StatusCode(StatusCodes.Status500InternalServerError, PikchaMessages.MESS_Status404ImageNotFound);

            }
        }


        [Authorize]
        [HttpPost("{userId}/promote")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PromoteUserToArtist(string userId)
        {
            try
            {
                var pikchaUser = await _userManager.GetUserAsync(this.User);
                if (pikchaUser == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, PikchaMessages.MESS_Status404_UserNotFound);
                }
                bool isInAlready = await _userManager.IsInRoleAsync(pikchaUser, PikchaConstants.PIKCHA_ROLES_ARTIST_NAME);
                if(isInAlready)
                {
                    return StatusCode(StatusCodes.Status404NotFound, PikchaMessages.MESS_Status404_UserAlreadyPromoted);
                }

                using (var streamReader = new HttpRequestStreamReader(Request.Body, Encoding.UTF8))
                using (var jsonReader = new JsonTextReader(streamReader))
                {
                    var json = await JObject.LoadAsync(jsonReader);

                    string signatureContent = (string)json["signatureContent"];
                    if (signatureContent.Contains(','))
                    {
                        signatureContent = signatureContent.Split(",")[1];
                    }
                    // upload signature file
                    ImageProcessingManager manager = new ImageProcessingManager(_hostingEnvironment, _configuration);
                    //string filePath = await manager.UploadToLocalDirectory(signatureFile, Guid.NewGuid().ToString(), ".jpg", StorageManager.FileCategory.Sign);
                    string orgFName = string.Empty;
                    string invFName = string.Empty;

                    await manager.ProcessSignatureFileAsync(signatureContent, ref orgFName, ref invFName);

                    pikchaUser.Sign = orgFName;
                    pikchaUser.InvSign = invFName;
                    await _pikchDbContext.SaveChangesAsync();

                    var result = await _userManager.AddToRoleAsync(pikchaUser, PikchaConstants.PIKCHA_ROLES_ARTIST_NAME);

                    if (result.Succeeded)
                    {
                        var pkUsr = _mapper.Map<AuthenticatedUserDTO>(pikchaUser);
                        var roles = await _userManager.GetRolesAsync(pikchaUser);
                        if (roles != null)
                        {
                            pkUsr.Roles = roles.ToList();
                        }
                        return Ok(pkUsr);
                    }
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error in promoting the user ");

                }  
            }
            catch (Exception ex)
            {
                Log.Error(ex, " Profile, PromoteUserToArtist, userId ={userId}", userId);
                return StatusCode(StatusCodes.Status500InternalServerError, PikchaMessages.MESS_Status500InternalServerError);

            }
        }


        [Authorize]
        [HttpGet("{userId}/signature")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetSignature(string userId)
        {
            try
            {
                var pikchaUser = await _userManager.GetUserAsync(this.User);
                if (pikchaUser == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, PikchaMessages.MESS_Status404_UserNotFound);
                }
                return Ok(new { OrgSig = pikchaUser.Sign ?? string.Empty, InvSig = pikchaUser.InvSign ?? string.Empty });

            }
            catch (Exception ex)
            {
                Log.Error(ex, " Profile, GetSignature, userId ={userId}", userId);
                return StatusCode(StatusCodes.Status500InternalServerError, PikchaMessages.MESS_Status500InternalServerError);

            }
        }
                  

        [Authorize]
        [HttpGet("{userId}/myinfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> LoggedInUserInfo( string userId)
        {
            try
            {
                var pikchaUser = await _userManager.GetUserAsync(this.User);
                if (pikchaUser == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, PikchaMessages.MESS_Status404_UserNotFound);
                }
                return ReturnOkOrErrorStatus(await GetAuthenticatedUserDTO(pikchaUser.Id));
                /*
                var userDB = await _pikchDbContext.PikchaUsers.Include("Images").Include("Following.Artist")
                    .FirstAsync(u => u.Id == pikchaUser.Id); // images -> last uploaded on, artists -> following
                
                var userDTO = _mapper.Map<AuthenticatedUserDTO>(pikchaUser);
                var roles = await _userManager.GetRolesAsync(pikchaUser);

                if (roles != null)
                {
                    userDTO.Roles = roles.ToList();
                }
                return ReturnOkOrErrorStatus(userDTO); */
            }
            catch(Exception ex)
            {
                Log.Error(ex, " Profile, LoggedInUserInfo, userId ={userId}", userId);
                return StatusCode(StatusCodes.Status500InternalServerError, PikchaMessages.MESS_Status500InternalServerError);
            }
        }

        [Authorize]
        [HttpPost("{userId}/artist/{artistId}/follow")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> FollowAnArtist(string artistId, string userId)
        {
            try
            {
                if (_pikchDbContext.PikchaUsers.Find(artistId) == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, PikchaMessages.MESS_Status404_ArtistNotFound);
                }

                var pikchaUser = await _userManager.GetUserAsync(this.User);
                if (pikchaUser == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, PikchaMessages.MESS_Status404_UserNotFound);
                }

                if (artistId == pikchaUser.Id)
                {
                    return StatusCode(StatusCodes.Status404NotFound, PikchaMessages.MESS_Status404_UserCantFollowHimself);
                }

                pikchaUser.Following.Add(new ArtistFollower() { ArtistsId = artistId });
                await _pikchDbContext.SaveChangesAsync();

                return ReturnOkOrErrorStatus(await GetAuthenticatedUserDTO(pikchaUser.Id));

                /*var qUser = _pikchDbContext.PikchaUsers.Include("Images").Include("Following.Artist")
                    .First(u => u.Id == pikchaUser.Id); // images -> last uploaded on, artists -> following


                var lgUSer = _mapper.Map<AuthenticatedUserDTO>(qUser);
                var roles = await _userManager.GetRolesAsync(qUser);

                if (roles != null)
                {
                    lgUSer.Roles = roles.ToList();
                }
                return ReturnOkOrErrorStatus(lgUSer); */
            }
            catch (Exception ex)
            {
                Log.Error(ex, " Profile, FollowAnArtist, userId ={userId}, artistId={artistId}", userId, artistId);
                return StatusCode(StatusCodes.Status500InternalServerError, PikchaMessages.MESS_Status500InternalServerError);
            }

        }

        [Authorize]
        [HttpPost("{userId}/artist/{artistId}/unfollow")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UnFollowAnArtist(string artistId, string userId)
        {
            try
            {
                if (_pikchDbContext.PikchaUsers.Find(artistId) == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, PikchaMessages.MESS_Status404_ArtistNotFound);
                }

                var pikchaUser = await _userManager.GetUserAsync(this.User);
                if (pikchaUser == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, PikchaMessages.MESS_Status404_UserNotFound);
                }

                var folArt = pikchaUser.Following.First(f => f.ArtistsId == artistId);
                if (folArt == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, PikchaMessages.MESS_Status404_UserAlreadyUnfollowed);
                }
                _pikchDbContext.Followers.Remove(folArt);
                await _pikchDbContext.SaveChangesAsync();

                //var lgUSer = await GetAuthenticatedUserDTO(pikchaUser.Id);
                return ReturnOkOrErrorStatus(await GetAuthenticatedUserDTO(pikchaUser.Id));
                /*var qUser = _pikchDbContext.PikchaUsers.Include("Images").Include("Following.Artist")
                    .First(u => u.Id == pikchaUser.Id); // images -> last uploaded on, artists -> following
                
                var lgUSer = _mapper.Map<AuthenticatedUserDTO>(qUser);
                var roles = await _userManager.GetRolesAsync(qUser);

                if (roles != null)
                {
                    lgUSer.Roles = roles.ToList();
                }
                return ReturnOkOrErrorStatus(lgUSer);*/
            }
            catch (Exception ex)
            {
                Log.Error(ex, " Profile, UnFollowAnArtist, userId ={userId}, artistId={artistId}", userId, artistId);
                return StatusCode(StatusCodes.Status500InternalServerError, PikchaMessages.MESS_Status500InternalServerError);
            }
        }

        private async Task<AuthenticatedUserDTO> GetAuthenticatedUserDTO(string userId)
        {
            var qUser = await _pikchDbContext.PikchaUsers.Include("Images").Include("Following.Artist")
                       .FirstAsync(u => u.Id == userId); // images -> last uploaded on, artists -> following

            var lgUSer = _mapper.Map<AuthenticatedUserDTO>(qUser);
            var roles = await _userManager.GetRolesAsync(qUser);

            if (roles != null)
            {
                lgUSer.Roles = roles.ToList();
            }
            return lgUSer;
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
        public string PhoneNumber { get; set; }
        //public Dictionary<string, string> Links { get; set; }

    }
    public class LinkVM
    {
        public string Type { get; set; }
        public string Url { get; set; }

    }


}