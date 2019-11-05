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
using Serilog;

namespace PikchaWebApp.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<ActionResult> UploadImage([FromForm] ImageViewModel imgViewModel )
        {
            try
            {  //
               PikchaUser loggedinUser = await _userManager.GetUserAsync(this.User);
                if(loggedinUser == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, PikchaMessages.MESS_Status404_UserNotFound);
                }

                if (string.IsNullOrEmpty(imgViewModel.Signature) || ! System.IO.File.Exists(PikchaConstants.PIKCHA_IMAGE_UPLOAD_ROOT_FOLDER +  imgViewModel.Signature))
                {
                    return StatusCode(StatusCodes.Status404NotFound, PikchaMessages.MESS_Status404SignatureNotFound);
                }
                if (imgViewModel.ImageFile == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, PikchaMessages.MESS_Status404ImageNotFound);
                }

                string imageId = Guid.NewGuid().ToString();
                PikchaImage pkImg = new PikchaImage() { Id = imageId };
                pkImg.CopyPropertiesFrom(imgViewModel);
                               
                ImageProcessingManager imgManager = new ImageProcessingManager(_hostingEnvironment, _configuration);

                bool status = await imgManager.ProcessAndUploadImageAsync(imageId, imgViewModel.ImageFile, imgViewModel.Signature, ref pkImg);
                
                if (status)
                {
                    try
                    {
                        pkImg.Artist = loggedinUser;
                        await _pikchDbContext.PikchaImages.AddAsync(pkImg);
                        await _pikchDbContext.SaveChangesAsync();

                        // add image tags
                        await AddImageTags(pkImg, imgViewModel.Tags);

                        // add new product owned by owner of the image
                        decimal price = 0;
                        decimal.TryParse(imgViewModel.Price, out price);
                        ImageProduct imgPrd = new ImageProduct()
                        {
                             IsSale = true,
                              Price = price,
                               Type = PikchaConstants.PIKCHA_PRODUCT_TYPE_OWNER,
                                Image = pkImg,
                                 Seller = loggedinUser
                        };
                        
                        await _pikchDbContext.ImageProducts.AddAsync(imgPrd);
                        await _pikchDbContext.SaveChangesAsync();                        
                        return StatusCode(StatusCodes.Status201Created);

                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, " Image, UploadImage, parameters: viewmodel={imgViewModel}, fileName={fileName}", imgViewModel != null ? "Not null" : "Null",  (imgViewModel != null  && imgViewModel.ImageFile != null)? imgViewModel.ImageFile.FileName : "Null"   );
                        return StatusCode(StatusCodes.Status500InternalServerError, PikchaMessages.MESS_Status500InternalServerError);
                    }

                }
                else
                {
                    Log.Error(" Image, UploadImage, Image processing is failed., parameters: viewmodel={imgViewModel}, fileName={fileName}", imgViewModel != null ? "Not null" : "Null", (imgViewModel != null && imgViewModel.ImageFile != null) ? imgViewModel.ImageFile.FileName : "Null");
                    return StatusCode(StatusCodes.Status500InternalServerError, PikchaMessages.MESS_Status500ImageProcessingError);
                }
            }
            catch(Exception ex)
            {
                Log.Error(ex, " Image, UploadImage, ");
                return StatusCode(StatusCodes.Status500InternalServerError, PikchaMessages.MESS_Status500InternalServerError);

            }

        }

        [HttpGet("{imageId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetById(string imageId)
        {
            try
            {
                var pkImg = await _pikchDbContext.PikchaImages.Include("Products").FirstAsync( im => im.Id == imageId);
                if(pkImg == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, PikchaMessages.MESS_Status404NotFound);

                }
                var image =  _mapper.Map<PikchaImageFilterDTO>(pkImg);
                return ReturnOkOrErrorStatus(image);
                
            }
            catch (Exception ex)
            {
                Log.Error(ex, " Image, GetById, ");
                return StatusCode(StatusCodes.Status500InternalServerError, PikchaMessages.MESS_Status500InternalServerError);
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

                List<Tag> tags = await _pikchDbContext.Tags.OrderBy(t => t.Name).ToListAsync();
                return ReturnOkOrErrorStatus(tags);

            }
            catch (Exception ex)
            {
                Log.Error(ex, " Image, Tags, ");
                return StatusCode(StatusCodes.Status500InternalServerError, PikchaMessages.MESS_Status500InternalServerError);
                
            }
        }
        
     

        [HttpPost("{imageId}/view")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> IncrementViewCount(string imageId)
        {
            try
            {
                try
                {
                    var imgVw = await _pikchDbContext.ImageViews.FirstAsync(i => i.PikchaImage.Id == imageId && i.Date == DateTime.Today.Date);
                    if (imgVw == null)
                    {
                        PikchaImage pImg = _pikchDbContext.PikchaImages.First(i => i.Id == imageId);
                        if (pImg != null)
                        {
                            _pikchDbContext.ImageViews.Add(new ImageView() { PikchaImage = pImg, Date = DateTime.Today, Count = 1 });
                        }
                    }
                    else
                    {
                        imgVw.Count = imgVw.Count + 1;
                    }
                }
                catch
                {
                    PikchaImage pImg = _pikchDbContext.PikchaImages.First(i => i.Id == imageId);
                    if (pImg != null)
                    {
                        _pikchDbContext.ImageViews.Add(new ImageView() { PikchaImage = pImg, Date = DateTime.Today, Count = 1 });
                    }
                }

                await _pikchDbContext.SaveChangesAsync();

                // get the totla pikcha view count
                var views = await _pikchDbContext.ImageViews.Where(i => i.PikchaImage.Id == imageId).SumAsync( c => c.Count);

                //return StatusCode(StatusCodes.Status201Created);
                return Ok(views);

            }
            catch (Exception ex)
            {
                Log.Error(ex, " Image, IncrementViewCount, ");
                return StatusCode(StatusCodes.Status500InternalServerError, PikchaMessages.MESS_Status500InternalServerError);

            }
            
        }

        // PRIVATE 
        private async Task AddImageTags(PikchaImage pkImg, List<string> tags)
        {
            try
            {
                if (tags == null)
                {
                    return;
                }
                List<ImageTag> imgTags = new List<ImageTag>();
                // get all tags from 
                var tagsInDb = _pikchDbContext.Tags.Where(c => tags.Contains(c.Name)).ToList(); // single DB query
                foreach (var tag in tags)
                {
                    var tagInDb = tagsInDb.SingleOrDefault(t => t.Name == tag); // runs in memory
                    if (tagInDb == null)
                    {
                        Tag newTag = new Tag() { Name = tag };
                        await _pikchDbContext.Tags.AddAsync(newTag);
                        imgTags.Add(new ImageTag() { Tag = newTag, PikchaImage = pkImg });

                    }
                    else
                    {
                        imgTags.Add(new ImageTag() { Tag = tagInDb, PikchaImage = pkImg });
                    }
                }

                await _pikchDbContext.ImageTags.AddRangeAsync(imgTags);
                await _pikchDbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Log.Error(ex, " Image, AddImageTags, ");

            }
        }


    }


    public class ImageViewModel
    {
        public string Title { get; set; } = string.Empty;

        public string Caption { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public int NumberOfPrint { get; set; } = 100;

        public IFormFile ImageFile { get; set; } 

        public string Signature { get; set; } = string.Empty;
        
        public List<string> Tags { get; set; }

        public string Price { get; set; } = "100";
    }



}