using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PikchaWebApp.Data;
using PikchaWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace PikchaWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        protected readonly PikchaDbContext _pikchDbContext;

        public FilterController(PikchaDbContext pikchDbContext)
        {
            _pikchDbContext = pikchDbContext;

        }

        [HttpGet("images")]
        public async Task<ReturnDataModel> Images(string Type="random", int Start=0, int Count=20 )
        {
            try
            {
                
                List<PikchaImage> images = _pikchDbContext.PikchaImages.Include(img => img.Artist).Skip(Start).Take(Count).OrderBy(r => Guid.NewGuid()).ToList();
                return new ReturnDataModel() { Data = images };
            }
            catch(Exception ex)
            {
                return new ReturnDataModel() { Statuscode = STATUS_CODES.ExceptionThrown.ToString(), Status = "Error Occured", Data = ex.Message };

            }
        }

        


        [HttpGet("artists")]
        public async Task<ReturnDataModel> Artists(string Type = "random", int Start = 0, int Count = 20)
        {
            try
            {                
               var artists = _pikchDbContext.PikchaUsers.Skip(Start).Take(Count).OrderBy(r => Guid.NewGuid()).ToList();
               return new ReturnDataModel() { Data = artists };
            }
            catch (Exception ex)
            {
                return new ReturnDataModel() { Statuscode = STATUS_CODES.ExceptionThrown.ToString(), Status = "Error Occured", Data = ex.Message };

            }



        }

        


    }

}