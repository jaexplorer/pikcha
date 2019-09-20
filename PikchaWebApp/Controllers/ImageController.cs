using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        // POST: api/image/upload
        [HttpPost("/upload")]
        public async Task<ReturnDataModel> UploadImage(ImageViewModel imgViewModel)
        {
            try
            {
                string imageId = Guid.NewGuid().ToString();
                PikchaImage pkImg = new PikchaImage();
                pkImg.CopyPropertiesFrom(imgViewModel);

                pkImg.PikchaImageId = imageId;
                ImageProcessingManager imgManager = new ImageProcessingManager(_hostingEnvironment, _configuration);

                bool status = imgManager.ResizeImage(imageId, imgViewModel.ImageFile, ref pkImg);
                if (status)
                {
                    try
                    {
                        Task<PikchaUser> loggedinUserTask = _userManager.GetUserAsync(this.User);

                        await Task.WhenAll(loggedinUserTask);

                        PikchaUser loggedinUser = loggedinUserTask.Result;
                        if (loggedinUserTask.IsCompleted)
                        {
                            await _pikchDbContext.AddAsync(pkImg);
                            return new ReturnDataModel() { Data = pkImg };

                        }
                    }
                    catch(Exception e)
                    {
                        return new ReturnDataModel() { Statuscode = "1901", Status = "Error Occured", Data = e.Message };

                    }

                }
            }
            catch(Exception e)
            {
                return new ReturnDataModel() { Statuscode = "1901", Status = "Error Occured", Data = e.Message };

            }

            return new ReturnDataModel() { Statuscode="1901", Status="Error Occured", Data = "" };
        }


        private void AddImageTags()
        {
            // get all tags from 
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