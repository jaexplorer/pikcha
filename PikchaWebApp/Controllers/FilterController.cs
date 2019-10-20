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
        public async Task<ActionResult> Images(string Type="random", int Start=0, int Count=20 )
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

            var art = _pikchDbContext.PikchaUsers.Include("Images").Include("Images.Views").ToList();
            
            //var artists100 = await _mapper.ProjectTo<PikchaImageFilterDTO>(_pikchDbContext.PikchaUsers.Include("Images").Include("Images.Views").OrderByDescending(im => im.Images.Select(v => v.Views.Count())).Skip(start).Take(count)).ToListAsync();
            var artists100 = await _mapper.ProjectTo<PikchaImageFilterDTO>(_pikchDbContext.PikchaUsers.Include("Images").Include("Images.Views").OrderByDescending(im => im.Images.Count()).Skip(start).Take(count)).ToListAsync();
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