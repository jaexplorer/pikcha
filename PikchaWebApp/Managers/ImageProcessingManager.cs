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

        protected readonly string _watermark_img = PikchaConstants.PIKCHA_IMAGE_UPLOAD_ROOT_FOLDER +  "img/watermark-logo.png";

        public ImageProcessingManager(IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        public Task<bool> ProcessAndUploadImageAsync(string imageId, IFormFile formFileInfo, string signatureFile, ref PikchaImage pkImage)
        {
            return Task.FromResult<bool>(ProcessAndUploadImage(imageId, formFileInfo, signatureFile, ref pkImage));
        }

        private bool ProcessAndUploadImage(string imageId, IFormFile formFileInfo, string signatureFile, ref PikchaImage pkImage)
        {            
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    //await formFileInfo.CopyToAsync(memoryStream);
                    formFileInfo.CopyTo(memoryStream);

                    using (MagickImage image = new MagickImage(memoryStream.ToArray()))
                    {
                        using (MagickImage signatureImg = new MagickImage( PikchaConstants.PIKCHA_IMAGE_UPLOAD_ROOT_FOLDER +  signatureFile))
                        {
                            pkImage.Width = image.Width;
                            
                            pkImage.Height = image.Height;  
                            if(pkImage.Width > pkImage.Height)
                            {
                                signatureImg.Resize((int)(pkImage.Width / 10), 0); // 10 % width of the original image

                            }
                            else
                            {
                                signatureImg.Resize( 0,(int)(pkImage.Height / 10)); // 10 % width of the original image

                            }
                            image.Composite(signatureImg, Gravity.Southeast, CompositeOperator.Over);


                            MagickImage waterImage = (MagickImage)image.Clone();

                            if (image.Width > image.Height)
                            {

                                image.Resize(1024, 0);
                                if(pkImage.Width > 1920)
                                {
                                    waterImage.Resize(1920, 0);

                                }
                                else
                                {
                                    waterImage.Resize(pkImage.Width, 0);

                                }
                            }
                            else
                            {
                                image.Resize(0, 1024);
                                if(pkImage.Height> 1920)
                                {
                                    waterImage.Resize(0, 1920);
                                }
                                else
                                {
                                    waterImage.Resize(0, pkImage.Height);
                                }
                            }

                            StorageManager manager = new StorageManager(_hostingEnvironment, _configuration);
                            pkImage.Thumbnail = manager.UploadThumbnail(image, imageId, StorageManager.FileCategory.PikchaImage);

                            // Read the watermark that will be put on top of the image
                            using (MagickImage watermark = new MagickImage(_watermark_img))
                            {
                                if(waterImage.Width > waterImage.Height)
                                {
                                    watermark.Resize((int)(waterImage.Width / 5), 0); // 20 % width of the original image
                                }
                                else
                                {
                                    watermark.Resize(0, (int)(waterImage.Height / 5)); // 20 % width of the original image

                                }
                                // Draw the watermark in the center
                                waterImage.Composite(watermark, Gravity.Center, CompositeOperator.Over);
                                pkImage.Watermark = manager.UploadWaterMark(waterImage, imageId, StorageManager.FileCategory.PikchaImage);

                            }
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


        public Task<bool> ProcessSignatureFileAsync(string signatureContent, ref string sigFile, ref string invSigFile)
        {
            return Task.FromResult<bool>(ProcessSignatureFile(signatureContent, ref sigFile, ref invSigFile));
        }

        private bool ProcessSignatureFile(string signatureContent, ref string sigFile, ref string invSigFile)
        {
            try
            {  
               var imageDataByteArray = Convert.FromBase64String(signatureContent);

                using (var memoryStream = new MemoryStream(imageDataByteArray))
                {
                    memoryStream.Position = 0;
                    //await formFileInfo.CopyToAsync(memoryStream);
                    //formFileInfo.CopyTo(memoryStream);

                    using (MagickImage image = new MagickImage(memoryStream.ToArray()))
                    {

                        MagickImage revImg = (MagickImage)image.Clone();

                        //revImg.Negate();
                        revImg.Opaque(MagickColors.Black, MagickColors.White);

                        StorageManager manager = new StorageManager(_hostingEnvironment, _configuration);
                        string id = Guid.NewGuid().ToString();
                        sigFile = manager.UploadMagickImage(image, string.Empty, id + "-org", PikchaConstants.PIKCHA_SIGNATURE_SAVE_EXTENTION, StorageManager.FileCategory.Signature);
                        invSigFile = manager.UploadMagickImage(revImg, string.Empty, id + "-inv", PikchaConstants.PIKCHA_SIGNATURE_SAVE_EXTENTION, StorageManager.FileCategory.Signature);

                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool ProcessAvatarFile(string avatarContent, ref string avatarFile)
        {
            try
            {
                var imageDataByteArray = Convert.FromBase64String(avatarContent);

                using (var memoryStream = new MemoryStream(imageDataByteArray))
                {
                    memoryStream.Position = 0;
                    //await formFileInfo.CopyToAsync(memoryStream);
                    //formFileInfo.CopyTo(memoryStream);

                    using (MagickImage image = new MagickImage(memoryStream.ToArray()))
                    {
                        StorageManager manager = new StorageManager(_hostingEnvironment, _configuration);
                        string id = Guid.NewGuid().ToString();
                        avatarFile = manager.UploadMagickImage(image, string.Empty, id, PikchaConstants.PIKCHA_IMAGE_SAVE_EXTENTION, StorageManager.FileCategory.Avatar);

                        return true;
                    }
                }
            }
            catch (Exception e)
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
