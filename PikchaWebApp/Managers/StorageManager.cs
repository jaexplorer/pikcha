using ImageMagick;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PikchaWebApp.Managers
{
    public class StorageManager
    {
        public enum FileCategory { Avatar = 1, Signature = 2, PikchaImage =3 };

        protected readonly IWebHostEnvironment _hostingEnvironment;
        protected readonly IConfiguration _configuration;
        public StorageManager(IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }
        public async Task<string> UploadToLocalDirectory(IFormFile formFileInfo, FileCategory fileCategory)
        {
            var uploadPath = Path.Combine(_hostingEnvironment.WebRootPath, GetDirectory(fileCategory));

            ValidateDirectory(uploadPath);
            string filePath = Path.Combine(uploadPath, formFileInfo.FileName);

            var fileStream = new FileStream(filePath, FileMode.Create);
            fileStream.Position = 0;
            await formFileInfo.CopyToAsync(fileStream);
            return filePath;

        }

        public string UploadThumbnail(MagickImage image, string imageId, FileCategory fileCategory)
        {
            var uploadPath = Path.Combine(_hostingEnvironment.WebRootPath, GetDirectory(fileCategory), "Thumbnail") ;

            ValidateDirectory(uploadPath);
            string filePath = Path.Combine(uploadPath, imageId)  + ".jpeg";

            image.Write(filePath);

            return filePath;
        }

        public string UploadWaterMark(MagickImage image, string imageId, FileCategory fileCategory)
        {
            var uploadPath = Path.Combine(_hostingEnvironment.WebRootPath, GetDirectory(fileCategory), "Watermarks") ;

            ValidateDirectory(uploadPath);
            string filePath = Path.Combine(uploadPath, imageId) + ".jpeg";

            image.Write(filePath);

            return filePath;
        }

        private string GetDirectory(FileCategory fileCategory)
        {
            string uploadDirectory = @"\Uploads\Default";
            switch (fileCategory)
            {
                case FileCategory.Avatar: uploadDirectory = string.IsNullOrEmpty(_configuration["UploadDirectories.Avatar"]) ? @"\Uploads\Avatars" : _configuration["UploadDirectories.Avatar"]; break;
                case FileCategory.Signature: uploadDirectory = string.IsNullOrEmpty(_configuration["UploadDirectories.Signature"]) ? @"\Uploads\Signatures" : _configuration["UploadDirectories.Signature"]; break;
                case FileCategory.PikchaImage: uploadDirectory = string.IsNullOrEmpty(_configuration["UploadDirectories.PikchaImage"]) ? @"\Uploads\Images" : _configuration["UploadDirectories.PikchaImage"]; break;
            }
            try
            {
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }
                return uploadDirectory;
            }
            catch (Exception)
            {
                return uploadDirectory;
            }
        }

        private bool ValidateDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                try
                {
                    Directory.CreateDirectory(directoryPath);
                    return true;
                }
                catch
                {
                    return false;
                }
            }


            return true;
        }
    }
}
