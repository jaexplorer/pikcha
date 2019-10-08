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
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        protected readonly PikchaDbContext _pikchDbContext;
        private readonly IMapper _mapper;

        public FilterController(PikchaDbContext pikchDbContext, IMapper mapper)
        {
            _pikchDbContext = pikchDbContext;
            _mapper = mapper;

        }

        [HttpGet("images")]
        public async Task<ReturnDataModel> Images(string Type="random", int Start=0, int Count=20 )
        {
            try
            {
                if(Type == "pikcha100")
                {
                    var pikcha100imgs = _mapper.ProjectTo<Pikcha100ImageDTO>(_pikchDbContext.PikchaImages).OrderByDescending(im => im.TotalViews).Skip(Start).Take(Count).ToListAsync().Result;
                    return new ReturnDataModel() { Data = pikcha100imgs };
                }

                //List<PikchaImage> images = _pikchDbContext.PikchaImages.Include(img => img.Artist).Skip(Start).Take(Count).OrderBy(r => Guid.NewGuid()).ToList();
                var images = _mapper.ProjectTo<PikchaImageDTO>(_pikchDbContext.PikchaImages).ToListAsync().Result;
                return new ReturnDataModel() { Data = images};
            }
            catch(Exception ex)
            {
                return new ReturnDataModel() { Statuscode = (int) STATUS_CODES.ExceptionThrown, Status = "Error Occured", Data = ex.Message };

            }
        }

        
        [HttpGet("artists")]
        public async Task<ReturnDataModel> Artists(string Type = "random", int Start = 0, int Count = 20)
        {
            try
            {               // var bImg = art.BestImage;
                if (Type == "artists100")
                {
                    // DONT DELETE - This query is required to make sure that pikchaimageviews are included 
                    var art = _pikchDbContext.PikchaUsers.Include("PikchaImages").Include("PikchaImages.PikchaImageViews").ToList();

                    
                    var tmp2 = art[0].PikchaImages.Select(y => y.PikchaImageViews.Sum(z => z.Count));
                    var tmp3 = art[0].PikchaImages.Select(y => y.PikchaImageViews.Sum(z => z.Count)).Sum();

                    var artists100 = _mapper.ProjectTo<Pikcha100ArtistDTO>(_pikchDbContext.PikchaUsers.Include("PikchaImages").Include("PikchaImages.PikchaImageViews")).OrderByDescending(im => im.FirstName).Skip(Start).Take(Count).ToListAsync().Result;
                    return new ReturnDataModel() { Data = artists100 };
                }

                var artists = _pikchDbContext.PikchaUsers.Skip(Start).Take(Count).OrderBy(r => Guid.NewGuid()).ToList();
               return new ReturnDataModel() { Data = artists };
            }
            catch (Exception ex)
            {
                return new ReturnDataModel() { Statuscode = (int) STATUS_CODES.ExceptionThrown, Status = "Error Occured", Data = ex.Message };

            }
                       
        }
        
    }

}