using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    public class ImageController : PikchaBaseController
    {
        protected readonly IWebHostEnvironment _hostingEnvironment;
        protected readonly IConfiguration _configuration;
        protected readonly UserManager<PikchaUser> _userManager;
        protected readonly PikchaDbContext _pikchDbContext;
        private readonly IMapper _mapper;

        public ImageController(IWebHostEnvironment hostingEnvironment, IConfiguration configuration, UserManager<PikchaUser> userManager, PikchaDbContext pikchDbContext, IMapper mapper)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
            _userManager = userManager;
            _pikchDbContext = pikchDbContext;
            _mapper = mapper;
        }

        // POST: api/image/upload
        [HttpPost("upload")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<ActionResult> UploadImage([FromForm] ImageViewModel imgViewModel)
        {
            try
            {               
                string imageId = Guid.NewGuid().ToString();
                PikchaImage pkImg = new PikchaImage();
                pkImg.CopyPropertiesFrom(imgViewModel);
                pkImg.PikchaImageId = imageId;
                pkImg.UploadedAt = DateTime.Now;
                pkImg.ModifiedAt = DateTime.Now;

               
                ImageProcessingManager imgManager = new ImageProcessingManager(_hostingEnvironment, _configuration);

                bool status = imgManager.ResizeImage(imageId, imgViewModel.ImageFile, ref pkImg);

                // add image tags
                AddImageTags(ref pkImg, imgViewModel.Tags);

                if (status)
                {
                    try
                    {
                       //Task<PikchaUser> loggedinUserTask = _userManager.GetUserAsync(this.User);

                       // await Task.WhenAll(loggedinUserTask);

                        PikchaUser loggedinUser = await _userManager.GetUserAsync(this.User);

                        pkImg.Artist = loggedinUser;
                        await _pikchDbContext.AddAsync(pkImg);
                        await _pikchDbContext.SaveChangesAsync();
                        //return new ReturnDataModel() { Data = pkImg };
                        return StatusCode(StatusCodes.Status201Created);


                        /*if (loggedinUserTask.IsCompleted)
                        {
                            pkImg.Artist = loggedinUser;
                            await _pikchDbContext.AddAsync(pkImg);
                            await _pikchDbContext.SaveChangesAsync();
                            //return new ReturnDataModel() { Data = pkImg };
                            return Ok();
                            //return CreatedAtAction(nameof(GetById), new { imageId = pkImg.PikchaImageId }, pkImg);
                        } */

                    }
                    catch (Exception ex)
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                        //return new ReturnDataModel() { Statuscode = (int) STATUS_CODES.ExceptionThrown, Status = "Error Occured", Data = e.Message };

                    }

                }
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

                //return new ReturnDataModel() { Statuscode = (int)STATUS_CODES.ExceptionThrown, Status = "Error Occured", Data = e.Message };

            }
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Error Occurred.");

            //return new ReturnDataModel() { Statuscode= (int)STATUS_CODES.ExceptionThrown, Status="Error Occured", Data = "" };
        }

        [HttpGet("{imageId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetById(string imageId)
        {
            try
            {
                //var pkImg = await _pikchDbContext.PikchaImages.FirstAsync( im => im.PikchaImageId == imageId);
                var pkImg = await _pikchDbContext.PikchaImages.FirstAsync( im => im.PikchaImageId == imageId);
                if(pkImg == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, PikchaMessages.MESS_Status404NotFound);

                }
                var image =  _mapper.Map<PikchaImageDescriptionDTO>(pkImg);
                return ReturnOkOrErrorStatus(image);
                /*
                var imageTsk = _pikchDbContext.PikchaImages.Include(img => img.Artist).FirstAsync(i => i.PikchaImageId == imageId);

                await imageTsk;

                if (imageTsk.IsCompleted)
                {
                    return ReturnOkOrErrorStatus(imageTsk.Result);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, "Task is not completed.");
                */
                //return new ReturnDataModel() { Statuscode = (int)STATUS_CODES.ExceptionThrown, Status = "Success", Data = image };
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

                //return new ReturnDataModel() { Statuscode = (int)STATUS_CODES.ExceptionThrown, Status = "Error Occured", Data = ex.Message };
            }

        }


        [HttpGet("tags")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Tags()
        {
            try
            {

                List<PikchaTag> tags = await _pikchDbContext.ImageTags.OrderBy(t => t.Name).ToListAsync();
                return ReturnOkOrErrorStatus(tags);

               // return new ReturnDataModel() { Statuscode = (int)STATUS_CODES.ExceptionThrown, Status = "Error Occured", Data = tags };
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

               // return new ReturnDataModel() { Statuscode = (int)STATUS_CODES.ExceptionThrown, Status = "Error Occured", Data = ex.Message };

            }
        }
        
     

        [HttpPost("views/increment/{imageId}")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> IncrementViewCount(string imageId)
        {
            try
            {
                var tmp = _pikchDbContext.ImageViews.Count();
                try
                {
                    var imgVw = await _pikchDbContext.ImageViews.FirstAsync(i => i.PikchaImage.PikchaImageId == imageId && i.Date == DateTime.Today.Date);
                    if (imgVw == null)
                    {
                        PikchaImage pImg = _pikchDbContext.PikchaImages.First(i => i.PikchaImageId == imageId);
                        if (pImg != null)
                        {
                            _pikchDbContext.ImageViews.Add(new PikchaImageViews() { PikchaImage = pImg, Date = DateTime.Today, Count = 1 });
                        }
                    }
                    else
                    {
                        imgVw.Count = imgVw.Count + 1;
                    }
                }
                catch
                {
                    PikchaImage pImg = _pikchDbContext.PikchaImages.First(i => i.PikchaImageId == imageId);
                    if (pImg != null)
                    {
                        _pikchDbContext.ImageViews.Add(new PikchaImageViews() { PikchaImage = pImg, Date = DateTime.Today, Count = 1 });
                    }
                }

               

                await _pikchDbContext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }

        // PRIVATE 
        private void AddImageTags(ref PikchaImage pkImg, List<string> tags)
        {
            try
            {
                if (tags == null)
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
                        PikchaTag newTag = new PikchaTag() { Name = tag };
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
            catch (Exception e)
            {

            }


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