using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PikchaWebApp.Data;

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

        public async Task Images(ImageFilterModel flterModel)
        {
            _pikchDbContext.PikchaImages.ToList();     
            
        }
        
        [HttpGet("/image/{imageID}")]

        public async Task Image(string imageID)
        {
            _pikchDbContext.PikchaImages.First( i => i.ImageId == imageID);

        }

    }

    public class ImageFilterModel
    {
        public string Type { get; set; }
        public int? StartNum { get; set; }
        public string Count { get; set; }

    }

}