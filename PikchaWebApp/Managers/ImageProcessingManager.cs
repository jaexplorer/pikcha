using ImageMagick;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PikchaWebApp.Managers
{
    public class ImageProcessingManager
    {
        protected readonly IWebHostEnvironment _hostingEnvironment;
        protected readonly IConfiguration _configuration;
        public ImageProcessingManager(IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        public string ResizeImage(string imageId, IFormFile formFileInfo)
        {
            using (MagickImage image = new MagickImage(formFileInfo.FileName))
            {
                MagickImage waterImage = (MagickImage) image.Clone();

                if(image.Width > image.Height)
                {
                    
                    image.Resize(200, 0);
                    waterImage.Resize(600, 0);
                }
                else
                {
                    image.Resize(0, 200);
                    waterImage.Resize(0, 600);

                }

                StorageManager manager = new StorageManager(_hostingEnvironment, _configuration);
                string filename = manager.UploadThumbnail(image, imageId, StorageManager.FileCategory.PikchaImage);

                // Read the watermark that will be put on top of the image
                using (MagickImage watermark = new MagickImage("Magick.NET.png"))
                {
                    // Draw the watermark in the center
                    waterImage.Composite(watermark, Gravity.Center, CompositeOperator.Over);
                    string waterMarkfilename = manager.UploadWaterMark(waterImage, imageId, StorageManager.FileCategory.PikchaImage);

                }
                
                return filename;
            }
        }

    }
}
