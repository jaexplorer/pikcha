﻿using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
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
        public enum FileCategory { Avatar = 1, Signature = 2, PikchaImage =3, CoverPhoto =4 };

        protected readonly IWebHostEnvironment _hostingEnvironment;
        protected readonly IConfiguration _configuration;
        public StorageManager(IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }
        public async Task<string> UploadToLocalDirectory(IFormFile formFileInfo, string imageName, string imageExt, FileCategory fileCategory)
        {
            var uploadPath =GetDirectory(fileCategory);

            ValidateDirectory(PikchaConstants.PIKCHA_IMAGE_UPLOAD_ROOT_FOLDER  + uploadPath);
            string filePath = uploadPath + "/" + imageName + imageExt;
            //string fullPath = Path.GetFullPath(PikchaConstants.PIKCHA_IMAGE_UPLOAD_ROOT_FOLDER + filePath);

            var fileStream = new FileStream(PikchaConstants.PIKCHA_IMAGE_UPLOAD_ROOT_FOLDER  + filePath, FileMode.Create) ;
            fileStream.Position = 0;
            await formFileInfo.CopyToAsync(fileStream);
            return filePath;
        }

    
        public string UploadMagickImage(MagickImage image, string subDirectory, string imageName, string imageExt, FileCategory fileCategory)
        {
            var uploadPath = GetDirectory(fileCategory);
            if(!string.IsNullOrEmpty(subDirectory))
            {
                uploadPath = uploadPath + "/" + subDirectory;
            }

            ValidateDirectory(PikchaConstants.PIKCHA_IMAGE_UPLOAD_ROOT_FOLDER + uploadPath);
            string filePath =  uploadPath + "/" + imageName + imageExt;
            //string fullPath = Path.GetFullPath(PikchaConstants.PIKCHA_IMAGE_UPLOAD_ROOT_FOLDER + filePath);
            image.Write(PikchaConstants.PIKCHA_IMAGE_UPLOAD_ROOT_FOLDER + filePath);

            return filePath;
        }
        public string UploadThumbnail(MagickImage image, string imageId, FileCategory fileCategory)
        {
            var uploadPath = GetDirectory(fileCategory) + "/Thumbnail" ;

            ValidateDirectory(PikchaConstants.PIKCHA_IMAGE_UPLOAD_ROOT_FOLDER + uploadPath);
            string filePath = uploadPath + "/" + imageId  + ".jpg";

            image.Write(PikchaConstants.PIKCHA_IMAGE_UPLOAD_ROOT_FOLDER +  filePath);

            return filePath;
        }

        public string UploadWaterMark(MagickImage image, string imageId, FileCategory fileCategory)
        {
            var uploadPath = GetDirectory(fileCategory) + "/Watermarks" ;

            ValidateDirectory(PikchaConstants.PIKCHA_IMAGE_UPLOAD_ROOT_FOLDER  + uploadPath);
            string filePath = uploadPath + "/" + imageId + ".jpg";
            //string fullPath = Path.GetFullPath(PikchaConstants.PIKCHA_IMAGE_UPLOAD_ROOT_FOLDER + filePath);

            image.Write(PikchaConstants.PIKCHA_IMAGE_UPLOAD_ROOT_FOLDER + filePath);

            return filePath;
        }

        private string GetDirectory(FileCategory fileCategory)
        {
            string uploadDirectory = "Uploads/Default";
            switch (fileCategory)
            {
                case FileCategory.Avatar: uploadDirectory = string.IsNullOrEmpty(_configuration["UploadDirectories:Avatar"]) ? "Uploads/Avatars" : _configuration["UploadDirectories:Avatar"]; break;
                case FileCategory.Signature: uploadDirectory = string.IsNullOrEmpty(_configuration["UploadDirectories:Sign"]) ? "Uploads/Signatures" : _configuration["UploadDirectories:Sign"]; break;
                case FileCategory.PikchaImage: uploadDirectory = string.IsNullOrEmpty(_configuration["UploadDirectories:PikchaImage"]) ? "Uploads/Images" : _configuration["UploadDirectories:PikchaImage"]; break;
                case FileCategory.CoverPhoto: uploadDirectory = string.IsNullOrEmpty(_configuration["UploadDirectories:Cover"]) ? "Uploads/Covers" : _configuration["UploadDirectories:Cover"]; break;
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

        // upload files to AWS bucket
        public async Task UploadFileToS3(IFormFile file)
        {
            using (var client = new AmazonS3Client("yourAwsAccessKeyId", "yourAwsSecretAccessKey", RegionEndpoint.USEast1))
            {
                using (var newMemoryStream = new MemoryStream())
                {
                    file.CopyTo(newMemoryStream);

                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = file.FileName,
                        BucketName = "yourBucketName",
                        CannedACL = S3CannedACL.PublicRead
                    };

                    var fileTransferUtility = new TransferUtility(client);
                    await fileTransferUtility.UploadAsync(uploadRequest);
                }
            }
        }
    }
}
