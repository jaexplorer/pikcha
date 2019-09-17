using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class ImageController : ControllerBase
    {
        protected readonly IWebHostEnvironment _hostingEnvironment;
        protected readonly IConfiguration _configuration;
        protected readonly UserManager<PikchaUser> _userManager;
        protected readonly PikchaDbContext _pikchDbContext;

        public ImageController(IWebHostEnvironment hostingEnvironment, IConfiguration configuration, UserManager<PikchaUser> userManager, PikchaDbContext pikchDbContext)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
            _userManager = userManager;
            _pikchDbContext = pikchDbContext;
        }

        // POST: api/profile/avatar
        [HttpPost("/upload")]
        public async Task<ReturnDataModel> UploadImage(ImageViewModel imgViewModel)
        {
            Guid imageId = Guid.NewGuid();

            ImageProcessingManager imgManager = new ImageProcessingManager(_hostingEnvironment, _configuration);

            string fileName = imgManager.ResizeImage(imageId.ToString(), imgViewModel.ImageFile);
                       
            Task<PikchaUser> loggedinUserTask = _userManager.GetUserAsync(this.User);

            await Task.WhenAll(loggedinUserTask);

            PikchaUser loggedinUser = loggedinUserTask.Result;
            if (loggedinUserTask.IsCompleted)
            {
                PikchaImage pkImg = new PikchaImage();
                pkImg.CopyPropertiesFrom(imgViewModel);
                //pkImg. = filePath;
                await _pikchDbContext.AddAsync(pkImg);
            }
            return new ReturnDataModel() { Data = "" };
        }


    }


    public class ImageViewModel
    {
        public string Title { get; set; }

        public string Caption { get; set; }

        public string Location { get; set; }

        public int NumberOfPrint { get; set; }

        public IFormFile ImageFile { get; set; }
    }



}