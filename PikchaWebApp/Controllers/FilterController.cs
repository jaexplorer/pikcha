using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PikchaWebApp.Data;
using PikchaWebApp.Models;

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

        [HttpGet("/images")]
        public async Task<ReturnDataModel> Images(ImageFilterModel flterModel)
        {
            try
            {
                int start = flterModel.Start.Value;
                if (! flterModel.Start.HasValue)
                {
                    flterModel.Start = 0;
                }
                if (! flterModel.Count.HasValue)
                {
                    flterModel.Count = 20;
                }
                List<PikchaImage> images = _pikchDbContext.PikchaImages.Skip(flterModel.Start.Value).Take(flterModel.Count.Value).OrderBy(r => Guid.NewGuid()).ToList();
                return new ReturnDataModel() { Statuscode = STATUS_CODES.Success.ToString(), Status = "Error Occured", Data = images };
            }
            catch(Exception ex)
            {
                return new ReturnDataModel() { Statuscode = STATUS_CODES.ExceptionThrown.ToString(), Status = "Error Occured", Data = ex.Message };

            }



        }

        [HttpGet("/image/{imageId}")]
        public async Task<ReturnDataModel> Image(string imageId)
        {
            try
            {
                var image = _pikchDbContext.PikchaImages.First(i => i.PikchaImageId == imageId);

                return new ReturnDataModel() { Statuscode = STATUS_CODES.Success.ToString(), Status = "Success", Data = image };
            }
            catch(Exception ex)
            {
                return new ReturnDataModel() { Statuscode = STATUS_CODES.ExceptionThrown.ToString(), Status = "Error Occured", Data = ex.Message };
            }
            
        }


        [HttpGet("/artists")]
        public async Task<ReturnDataModel> Artists(ImageFilterModel flterModel)
        {
            try
            {
                if (!flterModel.Start.HasValue)
                {
                    flterModel.Start = 0;
                }
                if (! flterModel.Count.HasValue)
                {
                    flterModel.Count = 20;
                }
                var artists = _pikchDbContext.PikchaUsers.Skip(flterModel.Start.Value).Take(flterModel.Count.Value).OrderBy(r => Guid.NewGuid()).ToList();
                return new ReturnDataModel() { Statuscode = STATUS_CODES.Success.ToString(), Status = "Success", Data = artists };
            }
            catch (Exception ex)
            {
                return new ReturnDataModel() { Statuscode = STATUS_CODES.ExceptionThrown.ToString(), Status = "Error Occured", Data = ex.Message };

            }



        }

        [HttpGet("/artist/{artistId}")]
        public async Task<ReturnDataModel> Artist(string artistId)
        {
            try
            {
                var image = _pikchDbContext.PikchaUsers.First(i => i.Id == artistId);

                return new ReturnDataModel() { Statuscode = STATUS_CODES.Success.ToString(), Status = "Success", Data = image };
            }
            catch (Exception ex)
            {
                return new ReturnDataModel() { Statuscode = STATUS_CODES.ExceptionThrown.ToString(), Status = "Error Occured", Data = ex.Message };
            }

        }

    }

    public class ImageFilterModel
    {
        public string Type { get; set; }
        public int? Start { get; set; }
        
        public int? Count { get; set; }

    }

}