using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PikchaWebApp.Data;
using PikchaWebApp.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using PikchaWebApp.Managers;
using Serilog;
using Microsoft.AspNetCore.Identity;

namespace PikchaWebApp.Controllers
{
    public class FilterController : PikchaBaseController
    {
        protected readonly PikchaDbContext _pikchDbContext;
        private readonly IMapper _mapper;

        public FilterController(PikchaDbContext pikchDbContext, IMapper mapper)
        {
            _pikchDbContext = pikchDbContext;
            _mapper = mapper;

        }

        [HttpGet("images")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status416RangeNotSatisfiable)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Images(string Type="random",  int Start=0, int Count=20, string ArtistId = "" )
        {
            try
            {  
                switch(Type)
                {
                    case "pikcha100":
                        return await ProcessPikcha100(Start, Count);
                        //break;
                    case "artist100":
                        return await ProcessArtists100(Start, Count);
                    case "artistId":                       
                        return await ProcessArtistsId(ArtistId, Start, Count);

                    //break;
                    default:
                        return await ProcessRandomImages(Start, Count);
                        //break;                    

                }

                //if (Type == "pikcha100")
                //{
                //    //var pikcha100imgTsks = _mapper.ProjectTo<Pikcha100ImageDTO>(_pikchDbContext.Images).OrderByDescending(im => im.TotalViews).Skip(Start).Take(Count).ToListAsync();
                //    var pikcha100imgs = await _mapper.ProjectTo<PikchaImageFilterDTO>(_pikchDbContext.Images).OrderByDescending(im => im.TotalViews).Skip(Start).Take(Count).ToListAsync();
                //    return ReturnOkOrErrorStatus(pikcha100imgs);

                //    // return new ReturnDataModel() { Data = pikcha100imgs };

                //   /* await pikcha100imgTsks;

                //    if (pikcha100imgTsks.IsCompleted)
                //    {
                //        return ReturnOkOrErrorStatus(pikcha100imgTsks.Result);
                //    }

                //    return StatusCode(StatusCodes.Status500InternalServerError, "Task is not completed."); */
                //}

                ////List<PikchaImage> images = _pikchDbContext.Images.Include(img => img.Artist).Skip(Start).Take(Count).OrderBy(r => Guid.NewGuid()).ToList();
                //var images = await _mapper.ProjectTo<PikchaImageFilterDTO>(_pikchDbContext.Images).OrderByDescending(im => im.TotalViews).Skip(Start).Take(Count).ToListAsync();
                //return ReturnOkOrErrorStatus(images);
                ///*await imagesTsk;

                //if (imagesTsk.IsCompleted)
                //{
                //    return ReturnOkOrErrorStatus(imagesTsk.Result);
                //}

                //return StatusCode(StatusCodes.Status500InternalServerError, "Task is not completed."); */
            }
            catch(Exception ex)
            {
                Log.Debug(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        private async Task<ActionResult> ProcessPikcha100(int start, int count)
        {
            var pikcha100imgs = await _mapper.ProjectTo<PikchaImageFilterDTO>(_pikchDbContext.PikchaImages.Include("Views").OrderByDescending(i => i.Views.Count()).Skip(start).Take(count)).ToListAsync();
            return ReturnOkOrErrorStatus(pikcha100imgs);
        }

        private async Task<ActionResult> ProcessArtists100(int start, int count)
        {

            /*
             * dbContext.ChatMessages
    .Include(i => i.Author)
    .Include(i => i.Author.Claims)
    .Where(w => w.Author.Claims.Any(a => a.ClaimType == ClaimTypes.Role 
                                         && a.ClaimValue == "Voice"));

            */

            /*var users = (from u in _pikchDbContext.PikchaUsers
                         let query = (from ur in _pikchDbContext.Set<IdentityUserRole<string>>()
                                      where ur.UserId.Equals(u.Id)
                                      join r in _pikchDbContext.Roles on ur.RoleId equals r.Id
                                      where r.Name.Contains(PikchaConstants.PIKCHA_ROLES_ARTIST_NAME) select r.Name)
                         select u).ToList();

                         */

            //var artists100DbTest = await _pikchDbContext.PikchaUsers.Include("Images").Include("Images.Views").ToListAsync();
            //var artists100Dbttt = _pikchDbContext.PikchaUsers.Include("Images").Include("Images.Views").OrderByDescending(im => im.Images.Count()).Skip(start).Take(count).ToList();


            /*var artists100Db = (from u in _pikchDbContext.PikchaUsers
                        join id in _pikchDbContext.Set<IdentityUserRole<string>>()
                        on u.Id equals id.UserId
                        join ur in _pikchDbContext.Roles on id.RoleId equals ur.Id
                        where ur.Name.Contains(PikchaConstants.PIKCHA_ROLES_ARTIST_NAME)
                        join im in _pikchDbContext.PikchaImages on u.Id equals im.Artist.Id
                        join iv in _pikchDbContext.ImageViews on im.Id equals iv.PikchaImageId
                        select u ).Skip(start).Take(count); */


            /* var artists100Db = (from u in _pikchDbContext.PikchaUsers.Include("Images").Include("Images.Views")
                                 join id in _pikchDbContext.Set<IdentityUserRole<string>>()
                                 on u.Id equals id.UserId
                                 join ur in _pikchDbContext.Roles on id.RoleId equals ur.Id
                                 where ur.Name.Contains(PikchaConstants.PIKCHA_ROLES_ARTIST_NAME)
                                 select u); */
            //.Include(x => x.Tests.SelectMany(z => z.PupilsTests))


            /*var artists100 = (from u in _pikchDbContext.PikchaUsers.Include("Images").Include("Images.Views")
                              join id in _pikchDbContext.Set<IdentityUserRole<string>>()
                              on u.Id equals id.UserId
                              join ur in _pikchDbContext.Roles on id.RoleId equals ur.Id
                              where ur.Name.Contains(PikchaConstants.PIKCHA_ROLES_ARTIST_NAME) select u).Select
                              (user => new PikchaArtist100ImageFilterDTO()
                                {
                                    Artist = new PikchaArtistBaseDTO()
                                    {
                                        Id = user.Id,
                                        FName = user.FName,
                                        LName = user.LName,
                                        //Location = u.City + ", " + u.Country
                                    },
                                    TopImage = user.Images.OrderByDescending(v => v.Views.Sum(iv => iv.Count)).First()
                                }
                                    ).ToList(); */


            //var tmp2 = _pikchDbContext.PikchaUsers.Include("Images").Include("Images.Views").OrderByDescending(im => im.Images.Count()).Skip(start).Take(count).ToList();


            //var art = _pikchDbContext.PikchaUsers.Include(r => r.Roles).Where(r => r.Roles.Any(a => a.;
            //var art = _pikchDbContext.PikchaUsers.Include("Images").Include("Images.Views").ToList();

            //var artists100 = await _mapper.ProjectTo<PikchaImageFilterDTO>(_pikchDbContext.PikchaUsers.Include("Images").Include("Images.Views").OrderByDescending(im => im.Images.Select(v => v.Views.Count())).Skip(start).Take(count)).ToListAsync();
            //var artists100 = await _mapper.ProjectTo<PikchaArtist100ImageFilterDTO>(artists100Db).Skip(start).Take(count).ToListAsync();

            /*var tmp2 = context.PikchaUsers.Include(u => u.Images).ThenInclude(im => im.Views)
               .Select(user => new PikchaArtist100ImageFilterDTO()
               {
                   TopImage = user.Images.First()
               }).ToList();*/

            /*var tmp4 = (from user in context.PikchaUsers
                       join img in context.PikchaImages on user.Id equals img.Artist.Id
                       join iv in context.ImageViews on img.Id equals iv.PikchaImage.Id
                       group iv by iv.PikchaImage.Id into imgGp

                       select new
                       {
                           KeyS = userGp.Key,
                           TotView = userGp.Where(i => i.)
                       }

                       ).ToList(); */

            /*var tmp5 = (from user in context.PikchaUsers.Where(i => i.Images.Count > 0).OrderByDescending(i => i.Images.Sum(v => v.Views.Sum(c => c.Count)))
                        select new PikchaArtist100ImageFilterDTO()
                        {
                           Artist = new PikchaArtistBaseDTO()
                           {
                                Avatar = user.Avatar,
                                 FName = user.FName,
                                  LName = user.LName,
                                   Id = user.Id,
                                    Location = string.Concat( string.IsNullOrEmpty(user.City)? string.Empty : user.City + ", " , string.IsNullOrEmpty(user.Country) ? string.Empty : user.Country),
                                     Email = user.Email, 
                                     AggrImViews = (from img in user.Images
                                                    join iv in context.ImageViews on img.Id equals iv.PikchaImage.Id
                                                    select iv
                                            ).Sum(v => v.Count).ToString()
                           },
                            TopImage = user.Images.OrderByDescending(i => i.Views.Sum(v => v.Count)).First(),
                            Views = user.Images.OrderByDescending(i => i.Views.Sum(v => v.Count)).First().Views.Sum(v => v.Count).ToString()


                        }).Skip(0).Take(5).ToList(); */

            /*
             * AggrImViews = (from img in user.Images
                                            join iv in context.ImageViews on img.Id equals iv.PikchaImage.Id
                                            group iv by iv.PikchaImage.Artist.Id into imgGp
                                            select imgGp.Sum(v => v.Count)
                                            ).ToString()
                                            */

            /*new PikchaArtistBaseDTO()
                      {
                           AggrImViews = gpUser.Sum(i => i.Images.Count).ToString()
                      }*/
            /*var tmp3 = context.PikchaImages.Include("Views")
       .Select(user => new
       {
           ViewCount = user.Views.Sum(v => v.Count)
       }).ToList();

            var tmp1 = context.PikchaUsers.Include("Images").Include("Images.Views")
       .Select(user => new PikchaArtist100ImageFilterDTO()
       {
           TopImage = user.Images.First()
       }).ToList();  */
            var artists100 = await (from user in _pikchDbContext.PikchaUsers.Where(i => i.Images.Count > 0).OrderByDescending(i => i.Images.Sum(v => v.Views.Sum(c => c.Count)))
                        select new PikchaArtist100ImageFilterDTO()
                        {
                            Artist = new PikchaArtistBaseDTO()
                            {
                                Avatar = user.Avatar,
                                FName = user.FName,
                                LName = user.LName,
                                Id = user.Id,
                                Location = string.Concat(string.IsNullOrEmpty(user.City) ? string.Empty : user.City + ", ", string.IsNullOrEmpty(user.Country) ? string.Empty : user.Country),
                                Email = user.Email,
                                AggrImViews = (from img in user.Images
                                               join iv in _pikchDbContext.ImageViews on img.Id equals iv.PikchaImage.Id
                                               select iv
                                            ).Sum(v => v.Count).ToString()
                            },
                            TopImage = user.Images.OrderByDescending(i => i.Views.Sum(v => v.Count)).First(),
                            Views = user.Images.OrderByDescending(i => i.Views.Sum(v => v.Count)).First().Views.Sum(v => v.Count).ToString()


                        }).Skip(start).Take(count).ToListAsync();

            return ReturnOkOrErrorStatus(artists100);
        }


        private async Task<ActionResult> ProcessArtistsId(string artistId, int start, int count)
        {
            if(artistId == "")
            {
                return StatusCode(StatusCodes.Status404NotFound, PikchaMessages.MESS_Status404_ArtistNotFound); 
            }


            //var images = _pikchDbContext.PikchaImages.Include("Images.Views").Where( i => i.Artist.Id == artistId);

            //var artists100 = await _mapper.ProjectTo<PikchaImageFilterDTO>(_pikchDbContext.PikchaUsers.Include("Images").Include("Images.Views").OrderByDescending(im => im.Images.Select(v => v.Views.Count())).Skip(start).Take(count)).ToListAsync();
            //var tmp = _pikchDbContext.PikchaImages.Include("Artist").Include("Views").Where(i => i.Artist.Id == artistId).ToList();
            var artists100 =await  _mapper.ProjectTo<PikchaImageFilterDTO>(_pikchDbContext.PikchaImages.Include("Artist").Include("Views").Where(i => i.Artist.Id == artistId).AsQueryable().Skip(start).Take(count)).ToListAsync();
            return ReturnOkOrErrorStatus(artists100);
        }

        private async Task<ActionResult> ProcessRandomImages(int start, int count)
        {
            var images = await _mapper.ProjectTo<PikchaImageFilterDTO>(_pikchDbContext.PikchaImages.Skip(start).Take(count)).ToListAsync();
            return ReturnOkOrErrorStatus(images);
        }

        //[HttpGet("artists")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status416RangeNotSatisfiable)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<ActionResult> Artists(string Type = "random", int Start = 0, int Count = 20)
        //{
        //    try
        //    {               // var bImg = art.TopImage;
        //        if (Type == "artists100")
        //        {
        //            // DONT DELETE - This query is required to make sure that pikchaimageviews are included 
        //            var art = _pikchDbContext.PikchaUsers.Include("Images").Include("Images.Views").ToList();


        //            var artists100 =await _mapper.ProjectTo<PikchaArtistDTO>(_pikchDbContext.PikchaUsers.Include("Images").Include("Images.Views")).OrderByDescending(im => im.FName).Skip(Start).Take(Count).ToListAsync();
        //            return ReturnOkOrErrorStatus(artists100);
        //            /* await artists100Tsk;

        //            if (artists100Tsk.IsCompleted)
        //            {
        //                return ReturnOkOrErrorStatus(artists100Tsk.Result);
        //            }

        //            return StatusCode(StatusCodes.Status500InternalServerError, "Task is not completed."); */

            //            //return new ReturnDataModel() { Data = artists100 };
            //        }

            //        // DONT DELETE - This query is required to make sure that pikchaimageviews are included 
            //        var art2 = _pikchDbContext.PikchaUsers.Include("Images").Include("Images.Views").ToList();

            //        var artists = await _mapper.ProjectTo<PikchaArtistDTO>(_pikchDbContext.PikchaUsers.Include("Images").Include("Images.Views")).OrderByDescending(im => im.FName).Skip(Start).Take(Count).ToListAsync();
            //        return ReturnOkOrErrorStatus(artists);

            //       // var artists = await _pikchDbContext.PikchaUsers.Skip(Start).Take(Count).OrderBy(r => Guid.NewGuid()).ToListAsync();
            //       // return ReturnOkOrErrorStatus(artists);
            //        /*await artistTsk;

            //        if (artistTsk.IsCompleted)
            //        {
            //            return ReturnOkOrErrorStatus(artistTsk.Result);
            //        }

            //        return StatusCode(StatusCodes.Status500InternalServerError, "Task is not completed."); */
            //        //return new ReturnDataModel() { Data = artists };
            //    }
            //    catch (Exception ex)
            //    {
            //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            //       // return new ReturnDataModel() { Statuscode = (int) STATUS_CODES.ExceptionThrown, Status = "Error Occured", Data = ex.Message };

            //    }

            //}


        }

    }