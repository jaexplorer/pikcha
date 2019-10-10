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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Images(string Type="random", int Start=0, int Count=20 )
        {
            try
            {               

                if (Type == "pikcha100")
                {
                    //var pikcha100imgTsks = _mapper.ProjectTo<Pikcha100ImageDTO>(_pikchDbContext.PikchaImages).OrderByDescending(im => im.TotalViews).Skip(Start).Take(Count).ToListAsync();
                    var pikcha100imgs = await _mapper.ProjectTo<Pikcha100ImageDTO>(_pikchDbContext.PikchaImages).OrderByDescending(im => im.TotalViews).Skip(Start).Take(Count).ToListAsync();
                    return ReturnOkOrNotFound(pikcha100imgs);

                    // return new ReturnDataModel() { Data = pikcha100imgs };

                   /* await pikcha100imgTsks;

                    if (pikcha100imgTsks.IsCompleted)
                    {
                        return ReturnOkOrNotFound(pikcha100imgTsks.Result);
                    }

                    return StatusCode(StatusCodes.Status500InternalServerError, "Task is not completed."); */
                }

                //List<PikchaImage> images = _pikchDbContext.PikchaImages.Include(img => img.Artist).Skip(Start).Take(Count).OrderBy(r => Guid.NewGuid()).ToList();
                var images = await _mapper.ProjectTo<PikchaRandomImageDTO>(_pikchDbContext.PikchaImages).OrderByDescending(im => im.TotalViews).Skip(Start).Take(Count).ToListAsync();
                return ReturnOkOrNotFound(images);
                /*await imagesTsk;

                if (imagesTsk.IsCompleted)
                {
                    return ReturnOkOrNotFound(imagesTsk.Result);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "Task is not completed."); */
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }

        
        [HttpGet("artists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Artists(string Type = "random", int Start = 0, int Count = 20)
        {
            try
            {               // var bImg = art.TopImage;
                if (Type == "artists100")
                {
                    // DONT DELETE - This query is required to make sure that pikchaimageviews are included 
                    var art = _pikchDbContext.PikchaUsers.Include("PikchaImages").Include("PikchaImages.PikchaImageViews").ToList();


                    var artists100 =await _mapper.ProjectTo<Pikcha100ArtistDTO>(_pikchDbContext.PikchaUsers.Include("PikchaImages").Include("PikchaImages.PikchaImageViews")).OrderByDescending(im => im.FirstName).Skip(Start).Take(Count).ToListAsync();
                    return ReturnOkOrNotFound(artists100);
                    /* await artists100Tsk;

                    if (artists100Tsk.IsCompleted)
                    {
                        return ReturnOkOrNotFound(artists100Tsk.Result);
                    }

                    return StatusCode(StatusCodes.Status500InternalServerError, "Task is not completed."); */

                    //return new ReturnDataModel() { Data = artists100 };
                }

                // DONT DELETE - This query is required to make sure that pikchaimageviews are included 
                var art2 = _pikchDbContext.PikchaUsers.Include("PikchaImages").Include("PikchaImages.PikchaImageViews").ToList();

                var artists100 = await _mapper.ProjectTo<Pikcha100ArtistDTO>(_pikchDbContext.PikchaUsers.Include("PikchaImages").Include("PikchaImages.PikchaImageViews")).OrderByDescending(im => im.FirstName).Skip(Start).Take(Count).ToListAsync();
                return ReturnOkOrNotFound(artists100);

               // var artists = await _pikchDbContext.PikchaUsers.Skip(Start).Take(Count).OrderBy(r => Guid.NewGuid()).ToListAsync();
               // return ReturnOkOrNotFound(artists);
                /*await artistTsk;

                if (artistTsk.IsCompleted)
                {
                    return ReturnOkOrNotFound(artistTsk.Result);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "Task is not completed."); */
                //return new ReturnDataModel() { Data = artists };
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
               // return new ReturnDataModel() { Statuscode = (int) STATUS_CODES.ExceptionThrown, Status = "Error Occured", Data = ex.Message };

            }
                       
        }


    }

}