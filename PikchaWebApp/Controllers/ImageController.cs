using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [HttpPost("upload")]
        public async Task<ReturnDataModel> UploadImage([FromForm] ImageViewModel imgViewModel)
        {
            try
            {
                string imageId = Guid.NewGuid().ToString();
                PikchaImage pkImg = new PikchaImage();
                pkImg.CopyPropertiesFrom(imgViewModel);
                pkImg.Id = 19; // TO DO : id should be auto generated
                pkImg.PikchaImageId = imageId;
                pkImg.UploadedAt = DateTime.Now;
                pkImg.ModifiedAt = DateTime.Now;
                
                ImageProcessingManager imgManager = new ImageProcessingManager(_hostingEnvironment, _configuration);

                bool status = imgManager.ResizeImage(imageId, imgViewModel.ImageFile, ref pkImg);

                // add mimage tags
                AddImageTags(ref pkImg, imgViewModel.Tags);

                if (status)
                {
                    try
                    {
                        /*await _pikchDbContext.AddAsync(pkImg);
                        await _pikchDbContext.SaveChangesAsync();
                        return new ReturnDataModel() { Data = pkImg };*/
                        // return new ReturnDataModel() { Data = pkImg };

                       Task<PikchaUser> loggedinUserTask = _userManager.GetUserAsync(this.User);

                        await Task.WhenAll(loggedinUserTask);

                        PikchaUser loggedinUser = loggedinUserTask.Result;
                        if (loggedinUserTask.IsCompleted)
                        {
                            pkImg.Artist = loggedinUser;
                            await _pikchDbContext.AddAsync(pkImg);
                            await _pikchDbContext.SaveChangesAsync();
                            return new ReturnDataModel() { Data = pkImg };

                        }

                    }
                    catch(Exception e)
                    {
                        return new ReturnDataModel() { Statuscode = (int) STATUS_CODES.ExceptionThrown, Status = "Error Occured", Data = e.Message };

                    }

                }
            }
            catch(Exception e)
            {
                return new ReturnDataModel() { Statuscode = (int)STATUS_CODES.ExceptionThrown, Status = "Error Occured", Data = e.Message };

            }

            return new ReturnDataModel() { Statuscode= (int)STATUS_CODES.ExceptionThrown, Status="Error Occured", Data = "" };
        }

        [HttpGet("{imageId}")]
        public async Task<ReturnDataModel> Image(string imageId)
        {
            try
            {
                var image = _pikchDbContext.PikchaImages.Include(img => img.Artist).First(i => i.PikchaImageId == imageId);

                return new ReturnDataModel() { Statuscode = (int)STATUS_CODES.ExceptionThrown, Status = "Success", Data = image };
            }
            catch (Exception ex)
            {
                return new ReturnDataModel() { Statuscode = (int)STATUS_CODES.ExceptionThrown, Status = "Error Occured", Data = ex.Message };
            }

        }


        [HttpGet("tags")]
        public async Task<ReturnDataModel> Tags()
        {
            try
            {

                List<ImageTag> tags = _pikchDbContext.ImageTags.OrderBy(t => t.Name).ToList();
                return new ReturnDataModel() { Statuscode = (int)STATUS_CODES.ExceptionThrown, Status = "Error Occured", Data = tags };
            }
            catch (Exception ex)
            {
                return new ReturnDataModel() { Statuscode = (int)STATUS_CODES.ExceptionThrown, Status = "Error Occured", Data = ex.Message };

            }
        }


        private void AddImageTags(ref PikchaImage pkImg, List<string> tags)
        {
            try
            {
                if(tags == null)
                {
                    return;
                }
                List<PikchaImageTag> imgTags = new List<PikchaImageTag>();
                // get all tags from 
                var tagsInDb = _pikchDbContext.ImageTags.Where(c => tags.Contains(c.Name)).ToList(); // single DB query
                foreach (var tag in tags)
                {
                    var tagInDb = tagsInDb.SingleOrDefault(t => t.Name == tag); // runs in memory
                    if (tagInDb != null)
                    {
                        ImageTag newTag = new ImageTag() { Name = tag };
                        // _pikchDbContext.ImageTags.add();
                        _pikchDbContext.AddAsync(newTag);
                        _pikchDbContext.SaveChangesAsync();
                        imgTags.Add(new PikchaImageTag() { ImageTag = newTag, PikchaImage = pkImg });

                    }
                    else
                    {
                        imgTags.Add(new PikchaImageTag() { ImageTag = tagInDb, PikchaImage = pkImg });
                    }
                }
            }
            catch(Exception e)
            {

            }
            
            
        }


        [HttpPost("incrementviewcount")]
        public async Task<ReturnDataModel> IncrementViewCount(uint imageId)
        {
            try
            {
                var imgVwTsk = _pikchDbContext.ImageViews.FirstAsync(i => i.PikchaImage.Id == imageId && i.Date == DateTime.Today.Date);
                await imgVwTsk;
                var imgVw = imgVwTsk.Result;
                if ( imgVw == null)
                {
                    PikchaImage pImg = _pikchDbContext.PikchaImages.First(i => i.Id == imageId);
                    if(pImg != null)
                    {
                        _pikchDbContext.ImageViews.Add(new ImageView() { PikchaImage = pImg, Date = DateTime.Today, Count = 1 });
                    }
                }
                else
                {
                    imgVw.Count = imgVw.Count + 1;
                }

                await _pikchDbContext.SaveChangesAsync();

                return new ReturnDataModel();

            }
            catch(Exception e)
            {

            }

            return new ReturnDataModel();

        }



    }


    public class ImageViewModel
    {
        public string Title { get; set; }

        public string Caption { get; set; }

        public string Location { get; set; }

        public int NumberOfPrint { get; set; }

        public IFormFile ImageFile { get; set; }

        public List<string> Tags { get; set; }
    }



}