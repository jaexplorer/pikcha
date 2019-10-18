using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PikchaWebApp.Models
{
    public class PikchaImageDTOs
    {

    }

    public class PikchaRandomImageDTO : PikchaImageBaseDTO
    {
        public string Height { get; set; }

        public decimal Performance { get; set; }

        public int TotalPhotosSold { get; set; }
        public decimal AveragePrice { get; set; }

        public List<PikchaUser> Sellers { get; set; }
    }


    public class Pikcha100ImageDTO : PikchaImageBaseDTO
    {
        public string Caption { get; set; }
        public string Height { get; set; }
        public decimal Performance { get; set; }

        public int TotalImageSold { get; set; }
        public decimal AveragePrice { get; set; }

        public List<PikchaUser> Sellers { get; set; }
    }


    public class PikchaImageDescriptionDTO : PikchaImageBaseDTO
    {
        public string Caption { get; set; }

        public int NumberOfPrint { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

    }

    public class PikchaImageBaseDTO
    {
        public string PikchaImageId { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string ThumbnailFile { get; set; }
        public string WatermarkedFile { get; set; }
        public int TotalViews { get; set; }



        public string ArtistId { get; set; }
        public string ArtistFirstname { get; set; }
        public string ArtistLastname { get; set; }
        public string ArtistPercity { get; set; }
        public string ArtistPercountry { get; set; }
        public string ArtistAvatarfilename { get; set; }


    }

}
