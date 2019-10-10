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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<ActionResult> UploadImage([FromForm] ImageViewModel imgViewModel)
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

                // add image tags
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
                            //return new ReturnDataModel() { Data = pkImg };
                            return CreatedAtAction(nameof(GetById), new { imageId = pkImg.PikchaImageId }, pkImg);
                        }

                    }
                    catch(Exception ex)
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
                var image = await _mapper.ProjectTo<PikchaImageDescriptionDTO>(_pikchDbContext.PikchaImages).FirstAsync(i =>i.PikchaImageId == imageId);
                return ReturnOkOrNotFound(image);
                /*
                var imageTsk = _pikchDbContext.PikchaImages.Include(img => img.Artist).FirstAsync(i => i.PikchaImageId == imageId);

                await imageTsk;

                if (imageTsk.IsCompleted)
                {
                    return ReturnOkOrNotFound(imageTsk.Result);
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

                List<ImageTag> tags = await _pikchDbContext.ImageTags.OrderBy(t => t.Name).ToListAsync();
                return ReturnOkOrNotFound(tags);

               // return new ReturnDataModel() { Statuscode = (int)STATUS_CODES.ExceptionThrown, Status = "Error Occured", Data = tags };
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

               // return new ReturnDataModel() { Statuscode = (int)STATUS_CODES.ExceptionThrown, Status = "Error Occured", Data = ex.Message };

            }
        }
        
     

        [HttpPost("incrementviewcount")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> IncrementViewCount(uint imageId)
        {
            try
            {
                var imgVw = await _pikchDbContext.ImageViews.FirstAsync(i => i.PikchaImage.Id == imageId && i.Date == DateTime.Today.Date);

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

                return Ok();

            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            //return new ReturnDataModel();

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