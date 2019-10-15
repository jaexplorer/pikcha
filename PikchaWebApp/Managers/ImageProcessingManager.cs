using ImageMagick;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PikchaWebApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PikchaWebApp.Managers
{
    public class ImageProcessingManager
    {
        protected readonly IWebHostEnvironment _hostingEnvironment;
        protected readonly IConfiguration _configuration;

        protected readonly string _watermark_img = "Resources/Img/watermark-logo.png";

        public ImageProcessingManager(IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        public bool ResizeImage(string imageId, IFormFile formFileInfo, ref PikchaImage pkImage)
        {            
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    //await formFileInfo.CopyToAsync(memoryStream);
                    formFileInfo.CopyTo(memoryStream);

                    using (MagickImage image = new MagickImage(memoryStream.ToArray()))
                    {
                        pkImage.Width = image.Width;
                        pkImage.Height = image.Height;

                        MagickImage waterImage = (MagickImage)image.Clone();

                        if (image.Width > image.Height)
                        {

                            image.Resize(1600, 0);
                            waterImage.Resize(image.Width, 0);
                        }
                        else
                        {
                            image.Resize(0, 1600);
                            waterImage.Resize(0, image.Height);
                        }

                        StorageManager manager = new StorageManager(_hostingEnvironment, _configuration);
                        pkImage.ThumbnailFile = manager.UploadThumbnail(image, imageId, StorageManager.FileCategory.PikchaImage);

                        // Read the watermark that will be put on top of the image
                        using (MagickImage watermark = new MagickImage(_watermark_img))
                        {
                            //watermark.Resize(100, 0);
                            // Draw the watermark in the center
                            waterImage.Composite(watermark, Gravity.Center, CompositeOperator.Over);
                            pkImage.WatermarkedFile = manager.UploadWaterMark(waterImage, imageId, StorageManager.FileCategory.PikchaImage);

                        }
                        return true;
                    }
                }
             }
            catch(Exception e)
            {
                return false;
            }            
        }


        public bool ValidateImage(IFormFile formFileInfo)
        {

            return true;
        }

    }
}
